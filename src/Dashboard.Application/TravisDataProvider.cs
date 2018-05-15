using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces;
using Dashboard.Core.Interfaces.WebhookProviders;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using TravisApi;
using TravisApi.Models;
using TravisApi.Models.Responses;
using Stage = Dashboard.Core.Entities.Stage;

namespace Dashboard.Application
{
    /*
     * Build
     *   Stages
     *      Jobs
     */
    public class TravisDataProvider : ICiDataProvider, IProviderWithPipelineWebhook
    {
        public string Name => "Travis";

        public async Task<(IEnumerable<Pipeline> pipelines, int totalPages)> FetchNewestPipelines(string apiHost, string apiKey, string apiProjectId, int page, int perPage)
        {
            var apiClient = new TravisClient(apiHost, apiKey);

            var (builds, totalpages) = await apiClient.GetNewestBuilds(apiProjectId, page - 1, perPage, true, true);

            var fullInfoPipelines = builds.Select(MapBuildToPipeline);

            return (fullInfoPipelines, totalpages);
        }

        public async Task<Pipeline> FetchPipelineById(string apiHost, string apiKey, string apiProjectId, int pipelineId)
        {
            var apiClient = new TravisClient(apiHost, apiKey);

            var build = await apiClient.GetBuildById(pipelineId, true, true);

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

        public string GetProjectIdFromWebhookRequest(object body)
        {
            var collection = SimpleJson.SimpleJson.DeserializeObject<Dictionary<string, string>>(body.ToString(), new SnakeJsonSerializerStrategy());
            var travisWebhookResponse = SimpleJson.SimpleJson.DeserializeObject<WebhookResponse>(collection["payload"], new SnakeJsonSerializerStrategy());
            return travisWebhookResponse.Repository.Id.ToString();
        }

        public Pipeline ExtractPipelineFromWebhook(object body)
        {
            var collection = SimpleJson.SimpleJson.DeserializeObject<Dictionary<string, string>>(body.ToString(), new SnakeJsonSerializerStrategy());
            var travisWebhookResponse = SimpleJson.SimpleJson.DeserializeObject<WebhookResponse>(collection["payload"].ToString(), new SnakeJsonSerializerStrategy());
            return new Pipeline() { DataProviderPipelineId = travisWebhookResponse.Id };
        }

        private Pipeline MapBuildToPipeline(Build b)
        {
            //If build has no stages, map jobs as stages -> otherwise map stages
            var stages = !b.Stages.Any()
                ? b.Jobs.Select(j => new Stage { StageName = j.Number, StageStatus = MapTravisStatus(j.State) }).ToList()
                : b.Stages.Select(s => new Stage { StageName = s.Name, StageStatus = MapTravisStatus(s.State) }).ToList();

            return new Pipeline()
            {
                DataProviderPipelineId = b.Id,
                CommitTitle = b.Commit.Message,
                CommiterEmail = b.CreatedBy.Login,
                CommiterName = b.CreatedBy.Login,
                Ref = b.Branch.Name,
                Sha = b.Commit.Sha,
                CreatedAt = b.StartedAt,
                StartedAt = b.StartedAt,
                FinishedAt = b.FinishedAt,
                UpdatedAt = b.UpdatedAt,
                ProjectId = b.Repository.Slug,
                Status = MapTravisStatus(b.State),
                Stages = stages
            };
        }


        private Status MapTravisStatus(string travisStatus)
        {
            switch (travisStatus)
            {
                case "created":
                    return Status.Created;
                case "started":
                case "received":
                    return Status.Running;//Or Created, not sure when it is "received"
                case "errored":
                case "failed":
                    return Status.Failed;
                case "canceled":
                    return Status.Canceled;
                case "passed":
                    return Status.Success;
            }

            throw new InvalidEnumArgumentException($"{nameof(travisStatus)} {travisStatus}");
        }

        public string ExtractProjectIdFromPipelineWebhook(object body)
        {
            //return GetProjectIdFromWebhookRequest(body);
            var collection = SimpleJson.SimpleJson.DeserializeObject<Dictionary<string, string>>(body.ToString(), new SnakeJsonSerializerStrategy());
            var travisWebhookResponse = SimpleJson.SimpleJson.DeserializeObject<WebhookResponse>(collection["payload"], new SnakeJsonSerializerStrategy());
            return travisWebhookResponse.Repository.Id.ToString();
        }
    }
}
