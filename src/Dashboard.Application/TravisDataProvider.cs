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

        public Task<(IEnumerable<Pipeline> pipelines, int totalPages)> FetchNewestPipelines(string apiHost, string apiKey, string apiProjectId, int page, int perPage)
        {
            throw new NotImplementedException();
        }

        public async Task<Pipeline> FetchPipelineById(string apiHost, string apiKey, string apiProjectId, int pipelineId)
        {
            var apiClient = new TravisClient(apiHost, apiKey);

            var build = await apiClient.FetchBuildById(apiProjectId, pipelineId);

            return MapBuildToPipeline(build);
        }

        public Task<Pipeline> FetchPipeLineByBranch(string apiHost, string apiKey, string apiProjectId, string branchName)
        {
            throw new NotImplementedException();
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
                Ref = b.Commit.Ref,
                Sha = b.Commit.Sha,
                //CreatedAt = 
                StartedAt = b.StartedAt,
                FinishedAt = b.FinishedAt,
                UpdatedAt = b.UpdatedAt,
                ProjectId = b.Repository.Slug,
                Status = MapTravisStatus(b.State),
                Stages = (List<Stage>) b.Stages.Select(s => new Stage()
                {
                    Id = s.Id,
                    StageName = s.Name,
                    StageStatus = MapTravisStatus(s.State)
                })
            };
        }


    private Status MapTravisStatus(string gitlabStatus)
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
                case "canceled":
                    return Status.Canceled;

            }

            throw new InvalidEnumArgumentException($"{nameof(gitlabStatus)} {gitlabStatus}");
        }

    }
}
