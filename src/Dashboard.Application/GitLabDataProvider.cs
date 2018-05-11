using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dashboard.Application.GitLabApi;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces;
using Dashboard.Core.Interfaces.Repositories;
using Newtonsoft.Json.Linq;

namespace Dashboard.Application
{
    public class GitLabDataProvider : ICiDataProvider
    {
        public string Name => "GitLab";

        public async Task<IEnumerable<Pipeline>> FetchPipelines(string apiHost, string apiKey, string apiProjectId, int limit, string branchName = default(string))
        {
            var apiClient = new GitLabClient(apiHost, apiKey);

            var briefInfoPipelines = await apiClient.GetBriefPipelines(apiProjectId, numberOfPipelines: limit, branchName: branchName);
            var tasks = briefInfoPipelines.Select(bp => FetchPipelineById(apiHost, apiKey, apiProjectId, bp.Id));

            var fullInfoPipelines = await Task.WhenAll(tasks);
            return fullInfoPipelines;
        }

        public async Task<(IEnumerable<Pipeline> pipelines, int totalPages)> FetchNewestPipelines(string apiHost, string apiKey, string apiProjectId, int page, int perPage)
        {
            var apiClient = new GitLabClient(apiHost, apiKey);

            var apiResult = await apiClient.FetchNewestPipelines(apiProjectId, page, perPage);

            var fullInfoPipelines = await Task.WhenAll(apiResult.pipelines.Select(p => FetchPipelineById(apiHost, apiKey, apiProjectId, p.Id)));

            return (fullInfoPipelines, apiResult.totalPages);
        }

        public async Task<Pipeline> FetchPipelineById(string apiHost, string apiKey, string apiProjectId, int pipelineId)
        {
            var apiClient = new GitLabClient(apiHost, apiKey);

            var pipeline = await apiClient.GetPipelineById(apiProjectId, pipelineId);
            var pipelineCommit = await apiClient.GetCommitBySHA(apiProjectId, pipeline.Sha);
            var stages = await GetStagesWithJobs(apiHost, apiKey, apiProjectId, pipelineId);

            return MapPipelineToEntity(pipeline, pipelineCommit, stages, apiHost, apiProjectId);
        }
        public async Task<Pipeline> FetchPipeLineByBranch(string apiHost, string apiKey, string apiProjectId, string branchName)
        {
            var apiClient = new GitLabClient(apiHost, apiKey);
            var branchPipe = await apiClient.GetPipelineByBranch(apiProjectId, branchName);

            return await FetchPipelineById(apiHost, apiKey, apiProjectId, branchPipe.Id);
        }

        private async Task<IEnumerable<Stage>> GetStagesWithJobs(string apiHost, string apiKey, string apiProjectId, int pipeId)
        {
            var apiClient = new GitLabClient(apiHost, apiKey);
            var jobs = await apiClient.GetJobs(apiProjectId, pipeId);

            var stages = jobs.GroupBy(j => j.Stage)
                .Select(stage =>
                    new Stage()
                    {
                        StageName = stage.Key,
                        StageStatus = stage.Any(p => p.Status == "running") ? MapGitlabStatus("running") :
                            (stage.Any(p => p.Status == "manual") ? MapGitlabStatus("manual") :
                                (stage.Any(p => p.Status == "failed") ? MapGitlabStatus("failed") :
                                    (stage.All(p => p.Status == "skipped")) ? MapGitlabStatus("skipped") :
                                    (stage.All(p => p.Status == "success") ? MapGitlabStatus("success") :
                                        MapGitlabStatus("created")
                                    ))),
                    });

            return stages;
        }
        private Pipeline MapPipelineToEntity(GitLabApi.Models.Pipeline pipeline, GitLabApi.Models.Commit pipelineCommit, IEnumerable<Stage> stages, string apiHost, string apiProjectId)
        {
            return new Pipeline
            {
                ProjectId = apiHost + "/" + apiProjectId,
                DataProviderPipelineId = pipeline.Id,
                Ref = pipeline.Ref,
                Sha = pipeline.Sha,
                Status = MapGitlabStatus(pipeline.Status),

                CommitTitle = pipelineCommit.Title,
                CommiterName = pipelineCommit.CommitterName,
                CommiterEmail = pipelineCommit.CommitterEmail,
                CreatedAt = pipeline.CreatedAt,
                UpdatedAt = pipeline.UpdatedAt,
                StartedAt = pipeline.StartedAt,
                FinishedAt = pipeline.FinishedAt,

                Stages = stages.ToList()
            };
        }
        private Status MapGitlabStatus(string gitlabStatus)
        {
            switch (gitlabStatus)
            {
                case "running":
                    return Status.Running;
                case "manual":
                    return Status.Manual;
                case "failed":
                    return Status.Failed;
                case "skipped":
                    return Status.Skipped;
                case "success":
                    return Status.Success;
                case "created":
                    return Status.Created;
            }

            throw new InvalidEnumArgumentException(nameof(gitlabStatus));
        }




        public async Task<IEnumerable<string>> SearchBranchInProject(string apiHost, string apiKey, string apiProjectId, string searchValue)
        {
            var apiClient = new GitLabClient(apiHost, apiKey);
            var apiBranches = await apiClient.SearchForBranchInProject(apiProjectId, searchValue);

            //Get list of branch names
            var branches = apiBranches.Select(b => b.Name);

            return branches;
        }

        public string GetProjectIdFromWebhookRequest(JObject body)
        {
            return body["project"]["id"].Value<string>();
        }
    }
}