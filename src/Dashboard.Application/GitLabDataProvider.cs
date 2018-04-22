using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dashboard.Application.GitLabApi;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces;
using Dashboard.Core.Interfaces.Repositories;

namespace Dashboard.Application
{
    public class GitLabDataProvider : ICiDataProvider
    {
        public string Name => "GitLab";

        public async Task<IEnumerable<Pipeline>> GetAllPipelines(string apiHost, string apiKey, string apiProjectId)
        {
            var apiClient = new GitLabClient(apiHost, apiKey);
            var apiPipelines = await apiClient.GetPipelines(apiProjectId, numberOfPipelines : 100);//Number of pipelines to download

            //TODO: change when automapper
            var pipelines = apiPipelines.Select(p =>
                new Pipeline
                {
                    DataProviderId = p.Id,
                    Sha = p.Sha,
                    Ref = p.Ref,
                    Status = p.Status
                }
            );

            return pipelines;
        }

        public async Task<Pipeline> GetBranchPipeLine(string apiHost, string apiKey, string apiProjectId, string branchName)
        {
            var apiClient = new GitLabClient(apiHost, apiKey);
            var branchPipe = await apiClient.GetPipelineByBranch(apiProjectId, branchName);

            return new Pipeline { DataProviderId = branchPipe.Id, Sha = branchPipe.Sha, Ref = branchPipe.Ref, Status = branchPipe.Status };
        }

        public async Task<IEnumerable<string>> SearchBranchInProject(string apiHost, string apiKey, string apiProjectId, string searchValue)
        {
            var apiClient = new GitLabClient(apiHost, apiKey);
            var apiBranches = await apiClient.SearchForBranchInProject(apiProjectId, searchValue);

            //Get list of branch names
            var branches = apiBranches.Select(b => b.Name );

            return branches;
        }

        public async Task<Pipeline> GetSpecificPipeline(string apiHost, string apiKey, string apiProjectId, string pipeId)
        {
            var apiClient = new GitLabClient(apiHost, apiKey);
            var pipeline = await apiClient.GetPipelineById(apiProjectId, pipeId);
            var pipelineCommit = await apiClient.GetCommitBySHA(apiProjectId, pipeline.Sha);
            var stages = await GetStagesWithJobs(apiHost, apiKey, apiProjectId, pipeId);

            return new Pipeline
            {
                ProjectId = apiHost + "/" + apiProjectId,
                DataProviderId = pipeline.Id,
                Ref = pipeline.Ref,
                Sha = pipeline.Sha,
                Status = pipeline.Status,

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

        private async Task<IEnumerable<Stage>> GetStagesWithJobs(string apiHost, string apiKey, string apiProjectId, string pipeId)
        {
            var apiClient = new GitLabClient(apiHost, apiKey);
            var jobs = await apiClient.GetJobs(apiProjectId, pipeId);

            var stages = jobs.GroupBy(j => j.Stage)
                                .Select(s =>
                                new Stage()
                                {
                                    StageName = s.Key,
                                    StageStatus = s.Any(p => p.Status == "running") ? "running" : (s.Any(p => p.Status == "manual") ? "manual" : (s.Any(p => p.Status == "failed") ? "failed" : (s.All(p => p.Status == "skipped")) ? "skipped" : (s.All(p => p.Status == "success") ? "success" : "created"))),
                                });

            return stages;
        }

        public async Task<string> GetProjectIdFromWebhookRequest(Stream body)
        {
            string tmp = new StreamReader(body).ReadToEnd();
            string flag = "\"project\":{";
            string newBody = tmp.Substring(tmp.IndexOf(flag), tmp.IndexOf("}"));
            string id = Regex.Match(newBody, @"\d+").Value;
            return id;
        }
    }
}