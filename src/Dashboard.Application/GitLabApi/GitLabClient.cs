using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Dashboard.Application.GitLabApi.Models;
using RestSharp;
using RestSharp.Extensions;

namespace Dashboard.Application.GitLabApi
{
    //https://github.com/emilianoeloi/gitlab-ci-dashboard/blob/master/src/gitlab.js
    public class GitLabClient
    {
        private RestClient Client { get; }

        public GitLabClient(string apiHost, string apiKey)
        {
            // Convert snake_casing to CamelCasing
            SimpleJson.SimpleJson.CurrentJsonSerializerStrategy = new SnakeJsonSerializerStrategy();

            Client = new RestClient
            {
                Authenticator = new ApiTokenAuthenticator(apiKey),
                BaseUrl = new Uri($"{apiHost}/api/v4/")
            };
        }

        public async Task<Pipeline> GetPipelineById(string projectId, string pipelineId)
        {
            var r = await GetPipelines(projectId, pipelineId : pipelineId);
            return r.FirstOrDefault();
        }

        public async Task<Pipeline> GetPipelineByBranch(string projectId, string pipelineBranch)
        {
            var r = await GetPipelines(projectId, branchName : pipelineBranch);
            return r.FirstOrDefault();
        }

        public async Task<IEnumerable<Pipeline>> GetPipelines(string projectId, string pipelineId = "", int numberOfPipelines = 20, string branchName = "")
        {
            var request = new RestRequest("projects/{projectId}/pipelines/{pipelineId}", Method.GET);
            request.AddUrlSegment("projectId", HttpUtility.UrlEncode(projectId));
            request.AddUrlSegment("pipelineId", pipelineId);

            request.AddQueryParameter("per_page", numberOfPipelines.ToString());

            if (!string.IsNullOrEmpty(branchName))
                request.AddQueryParameter("ref", branchName);

            var r = await Client.ExecuteTaskAsync<List<Pipeline>>(request);
            return r.Data;
        }

        public async Task<Branch> GetBranch(string projectId, string branchName)
        {
            var r = await GetBranches(projectId, branchName : branchName);
            return r.FirstOrDefault();
        }

        public async Task<IEnumerable<Branch>> GetBranches(string projectId, string branchName = "")
        {
            var request = new RestRequest("projects/{projectId}/repository/branches/{branchName}", Method.GET);
            request.AddUrlSegment("projectId", HttpUtility.UrlEncode(projectId));
            request.AddUrlSegment("branchName", branchName);

            var r = await Client.ExecuteTaskAsync<List<Branch>>(request);
            return r.Data;
        }

        public async Task<Commit> GetCommitBySHA(string projectId, string commitSHA)
        {
            var r = await GetCommits(projectId, commitSHA : commitSHA);
            return r.FirstOrDefault();
        }

        public async Task<IEnumerable<Commit>> GetCommits(string projectId, string commitSHA = "")
        {
            var request = new RestRequest("projects/{projectId}/repository/commits/{commitSHA}", Method.GET);
            request.AddUrlSegment("projectId", HttpUtility.UrlEncode(projectId));
            request.AddUrlSegment("commitSHA", commitSHA);

            var r = await Client.ExecuteTaskAsync<List<Commit>>(request);
            return r.Data;
        }
    }
}
