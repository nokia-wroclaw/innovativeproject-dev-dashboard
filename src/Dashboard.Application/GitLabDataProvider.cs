using System.Collections.Generic;
using System.Linq;
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

        public Task<IEnumerable<Pipeline>> GetAllPipelines(string apiHost, string apiKey, string apiProjectId)
        {
            var apiClient = new GitLabClient(apiHost, apiKey);
            var apiPipelines = apiClient.GetProjectPipelines(apiProjectId);

            //TODO: change when automapper
            var pipelines = apiPipelines.Select(p =>
                new Pipeline
                {
                    Id = p.Id,
                    Sha = p.Sha,
                    Ref = p.Ref,
                    Status = p.Status
                }
            );

            return Task.FromResult(pipelines);
        }
    }
}