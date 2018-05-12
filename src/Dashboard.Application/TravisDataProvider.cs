using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces;
using Newtonsoft.Json.Linq;
using TravisApi;
using TravisApi.Models;
using Stage = Dashboard.Core.Entities.Stage;

namespace Dashboard.Application
{
    public class TravisDataProvider : ICiDataProvider
    {
        public string Name => "Travis";
        public async Task<IEnumerable<Pipeline>> FetchPipelines(string apiHost, string apiKey, string apiProjectId, int limit, string branchName = default(string))
        {
            var apiClient = new TravisClient(apiHost, apiKey);

            var r = await apiClient.FetchBuilds(apiProjectId, 100);

            return r.Builds.Select(MapBuildToPipeline);
        }

        public async Task<(IEnumerable<Pipeline> pipelines, int totalPages)> FetchNewestPipelines(string apiHost, string apiKey, string apiProjectId, int page, int perPage)
        {
            var apiClient = new TravisClient(apiHost, apiKey);

            var apiResult = await apiClient.GetNewestBuilds(apiProjectId, page - 1, perPage); // -1 cuz in service pages are counted from 1

            var fullInfoPipelines = apiResult.builds.Select(MapBuildToPipeline);

            return (fullInfoPipelines, apiResult.totalPages);
        }

        public async Task<Pipeline> FetchPipelineById(string apiHost, string apiKey, string apiProjectId, int pipelineId)
        {
            var apiClient = new TravisClient(apiHost, apiKey);

            var build = await apiClient.FetchBuildById(apiProjectId, pipelineId);

            return MapBuildToPipeline(build);
        }

        public async Task<Pipeline> FetchPipeLineByBranch(string apiHost, string apiKey, string apiProjectId, string branchName)
        {
            var apiClient = new TravisClient(apiHost, apiKey);
            var branch = await apiClient.GetBranch(apiProjectId, branchName);

            //TODO: branch last build may be null?
            return await FetchPipelineById(apiHost, apiKey, apiProjectId, branch.LastBuild.Id);
        }

        public Task<IEnumerable<string>> SearchBranchInProject(string apiHost, string apiKey, string apiProjectId, string searchValue)
        {
            throw new NotImplementedException();
        }

        public string GetProjectIdFromWebhookRequest(JObject body)
        {
            throw new NotImplementedException();
        }

        private Pipeline MapBuildToPipeline(Build b)
        {
            return new Pipeline()
            {
                DataProviderPipelineId = b.Id,
                CommitTitle = b.Commit.Message,
                CommiterEmail = b.CreatedBy.Login,
                CommiterName = b.CreatedBy.Login,
                Ref = b.Branch.Name,
                Sha = b.Commit.Sha,
                //CreatedAt = 
                StartedAt = b.StartedAt,
                FinishedAt = b.FinishedAt,
                UpdatedAt = b.UpdatedAt,
                ProjectId = b.Repository.Slug,
                Status = MapTravisStatus(b.State),
                Stages = b.Stages.Select(s => new Stage()
                {
                    StageName = s.Name,
                    StageStatus = MapTravisStatus(s.State)
                }).ToList()
            };
        }


    private Status MapTravisStatus(string travisStatus)
        {
            switch (travisStatus)
            {
                case "started":
                    return Status.Running;
                case "manual":
                    return Status.Manual;
                case "failed":
                    return Status.Failed;
                case "skipped":
                    return Status.Skipped;
                case "created":
                    return Status.Created;
                case "canceled":
                    return Status.Canceled;
                case "passed":
                    return Status.Success;

            }

            throw new InvalidEnumArgumentException($"{nameof(travisStatus)} {travisStatus}");
        }

    }
}
