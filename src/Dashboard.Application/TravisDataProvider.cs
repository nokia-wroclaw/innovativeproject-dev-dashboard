using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces;
using Newtonsoft.Json.Linq;
using TravisApi;

namespace Dashboard.Application
{
    public class TravisDataProvider : ICiDataProvider
    {
        public string Name => "Travis";

        public async Task<IEnumerable<Pipeline>> GetAllPipelines(string apiHost, string apiKey, string apiProjectId)
        {
            var apiClient = new TravisClient(apiHost, apiKey);

            var r = await apiClient.GetBuilds(apiProjectId, 100);

            return r.Builds.Select(b => new Pipeline()
            {
                DataProviderId = b.Id,
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
                //TODO: map state to status, change Status to enum
                Status = b.State,
                Stages = (List<Stage>)b.Stages.Select(s => new Stage()
                {
                    Id = s.Id,
                    StageName = s.Name,
                    //TODO: map state to status, change Status to enum
                    StageStatus = s.State
                })
            });
        }

        public Task<Pipeline> GetBranchPipeLine(string apiHost, string apiKey, string apiProjectId, string branchName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> SearchBranchInProject(string apiHost, string apiKey, string apiProjectId, string searchValue)
        {
            throw new NotImplementedException();
        }

        public Task<Pipeline> GetSpecificPipeline(string apiHost, string apiKey, string apiProjectId, string pipeId)
        {
            throw new NotImplementedException();
        }

        public string GetProjectIdFromWebhookRequest(JObject body)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Pipeline>> GetLatestPipelines(string apiHost, string apiKey, string apiProjectId, int quantity, IEnumerable<Pipeline> localPipes,
            IEnumerable<string> staticPipes)
        {
            throw new NotImplementedException();
        }
    }
}
