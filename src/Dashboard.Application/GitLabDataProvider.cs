using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dashboard.Application.GitLabApi;
using GitLabModel = Dashboard.Application.GitLabApi.Models.Webhooks;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces;
using Dashboard.Core.Interfaces.CiProviders;
using Dashboard.Core.Interfaces.Repositories;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


namespace Dashboard.Application
{
    public class GitLabDataProvider : ICiDataProvider, ICiWebhookProvider
    {
        public string Name => "GitLab";

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
        
        public async Task<bool> TestApiCredentials(string apiHost, string apiKey)
        {
            var apiClient = new GitLabClient(apiHost, apiKey);

            var userResponse = await apiClient.GetUser();

            // GetUser will throw exception when credidentials are not valid
            // so its safe to asume apikey is valid
            return true;
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
                        //StageStatus = CalculateStageStatus(stage.Select(p => new Job() { DataProviderJobId = p.Id, Status = MapGitlabStatus(p.Status) }).ToList()),
                        Jobs = stage.Select(p => new Job() { DataProviderJobId = p.Id, Status = MapGitlabStatus(p.Status), StageName = p.Stage }).ToList()
                    });

            return stages;
        }
        private Status CalculateStageStatus(ICollection<Job> jobs)
        {
            //Need to check for "canceled"?
            return jobs.Any(p => p.Status == MapGitlabStatus("running")) ? MapGitlabStatus("running") :
                            (jobs.Any(p => p.Status == MapGitlabStatus("manual")) ? MapGitlabStatus("manual") :
                                (jobs.Any(p => p.Status == MapGitlabStatus("failed")) ? MapGitlabStatus("failed") :
                                    (jobs.All(p => p.Status == MapGitlabStatus("skipped"))) ? MapGitlabStatus("skipped") :
                                    (jobs.All(p => p.Status == MapGitlabStatus("success")) ? MapGitlabStatus("success") :
                                        MapGitlabStatus("created")
                                    )));
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
                case "failed":
                    return Status.Failed;
                case "manual":
                case "skipped":
                case "canceled":
                case "manual":
                    return Status.Canceled;
                case "success":
                    return Status.Success;
                case "pending":
                case "created":
                    return Status.Created;

            }

            throw new InvalidEnumArgumentException($"{nameof(gitlabStatus)} {gitlabStatus}");
        }

        public async Task<IEnumerable<string>> SearchBranchInProject(string apiHost, string apiKey, string apiProjectId, string searchValue)
        {
            var apiClient = new GitLabClient(apiHost, apiKey);
            var apiBranches = await apiClient.SearchForBranchInProject(apiProjectId, searchValue);

            //Get list of branch names
            var branches = apiBranches.Select(b => b.Name);

            return branches;
        }


        public DataProviderJobInfo ExtractJobInfo(JObject requestBody)
        {
            return new DataProviderJobInfo()
            {
                ProjectId = requestBody["ProjectId"].Value<string>(),
                JobId = requestBody["BuildId"].Value<int>(),
                Status = MapGitlabStatus(requestBody["BuildStatus"].Value<string>()),
                ProviderName = Name,
            };
        }

        public DataProviderPipelineInfo ExtractPipelineInfo(JObject requestBody)
        {
            var objectAttributes = requestBody["object_attributes"];

            return new DataProviderPipelineInfo()
            {
                ProjectId = requestBody["project"]["id"].Value<string>(),
                PipelineId = objectAttributes["id"].Value<string>(),
                Status = MapGitlabStatus(objectAttributes["status"].Value<string>()),
                ProviderName = Name
            };
        }
    }
}
