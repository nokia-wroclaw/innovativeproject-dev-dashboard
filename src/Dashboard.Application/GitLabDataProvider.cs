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

        public async Task<IEnumerable<Pipeline>> GetAllPipelines(string apiHost, string apiKey, string apiProjectId)
        {
            var apiClient = new GitLabClient(apiHost, apiKey);
            var apiPipelines = await apiClient.GetPipelines(apiProjectId, numberOfPipelines : 100);//Number of pipelines to download

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

            return pipelines;
        }

        public async Task<Pipeline> GetBranchPipeLine(string apiHost, string apiKey, string apiProjectId, string branchName)
        {
            var apiClient = new GitLabClient(apiHost, apiKey);
            var branchPipe = await apiClient.GetPipelineByBranch(apiProjectId, branchName);

            return new Pipeline { Id = branchPipe.Id, Sha = branchPipe.Sha, Ref = branchPipe.Ref, Status = branchPipe.Status };
        }

        public async Task<IEnumerable<string>> GetAllProjectBranchNames(string apiHost, string apiKey, string apiProjectId)
        {
            var apiClient = new GitLabClient(apiHost, apiKey);
            var apiBranches = await apiClient.GetBranches(apiProjectId);

            //Get list of branch names
            var branches = apiBranches.Select(b => b.Name );

            return branches;
        }

        //public async Task<IEnumerable<string>> GetAllBranchNames
    }
}