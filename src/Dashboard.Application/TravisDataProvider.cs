using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces;
using Dashboard.Core.Interfaces.CiProviders;
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
    public class TravisDataProvider : ICiDataProvider, ICiWebhookProvider
    {
        public string Name => "Travis";

        public async Task<(IEnumerable<Pipeline> pipelines, int totalPages)> FetchNewestPipelines(string apiHost, string apiKey, string apiProjectId, int page, int perPage)
        {
            var apiClient = new TravisClient(apiHost, apiKey);

            var (builds, totalpages) = await apiClient.GetNewestBuilds(apiProjectId, page - 1, perPage, true, true, true);

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

        public async Task<bool> TestApiCredentials(string apiHost, string apiKey)
        {
            var apiClient = new TravisClient(apiHost, apiKey);

            var userResponse = await apiClient.GetUser();

            return userResponse.Permissions.Read;
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
                ? b.Jobs.Select(j => new Stage { StageName = j.Number/*, StageStatus = MapTravisStatus(j.State)*/ }).ToList()
                : b.Stages.Select(s => new Stage { StageName = s.Name/*, StageStatus = MapTravisStatus(s.State)*/ }).ToList();

            List<Stage> stagesList = new List<Stage>();
            if(!b.Stages.Any())
            {
                stagesList = b.Jobs.Select(j => new Stage { StageName = j.Number/*, StageStatus = MapTravisStatus(j.State)*/ }).ToList();
            }
            else
            {
                //foreach (var stage in b.Stages)
                for(int i = b.Stages.Count - 1; i >= 0; i--)    //Travis gives stages in reversed order
                {
                    var stage = b.Stages[i];
                    var s = new Stage { StageName = stage.Name/*, StageStatus = MapTravisStatus(stage.State)*/ };
                    var jobs = stage.Jobs.Select(p => new Core.Entities.Job
                    {
                        DataProviderJobId = p.Id,
                        StageName = stage.Name,
                        Status = MapTravisStatus(b.Jobs.FirstOrDefault(j => j.Id == p.Id).State)
                    });
                    s.Jobs = jobs.ToList();
                    stagesList.Add(s);
                }
            }

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
                Stages = stagesList/*stages*/
            };
        }

        private IEnumerable<Core.Entities.Job> GetJobsForStage(string stageName)
        {
            return null;
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

        public DataProviderJobInfo ExtractJobInfo(JObject requestBody)
        {
            throw new NotImplementedException();
        }

        public DataProviderPipelineInfo ExtractPipelineInfo(JObject requestBody)
        {
            var collection = SimpleJson.SimpleJson.DeserializeObject<Dictionary<string, string>>(requestBody.ToString(), new SnakeJsonSerializerStrategy());
            var travisWebhookResponse = SimpleJson.SimpleJson.DeserializeObject<WebhookResponse>(collection["payload"].ToString(), new SnakeJsonSerializerStrategy());

            return new DataProviderPipelineInfo()
            {
                Status = MapTravisStatus(travisWebhookResponse.State),
                ProviderName = Name,
                ProjectId = travisWebhookResponse.Repository.Id.ToString(),
                PipelineId = travisWebhookResponse.Id.ToString()
            };
        }
    }
}
