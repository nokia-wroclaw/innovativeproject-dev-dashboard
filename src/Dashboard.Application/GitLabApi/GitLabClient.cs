using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Dashboard.Application.GitLabApi.Models;
using Dashboard.Core.Exceptions;
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

        public Task<IEnumerable<Pipeline>> GetPipelines(string projectId, string pipelineId = "", int numberOfPipelines = 20, string branchName = "")
        {
            var request = new RestRequest("projects/{projectId}/pipelines/{pipelineId}", Method.GET);
            request.AddUrlSegment("projectId", HttpUtility.UrlEncode(projectId));
            request.AddUrlSegment("pipelineId", pipelineId);

            request.AddQueryParameter("per_page", numberOfPipelines.ToString());

            if (!string.IsNullOrEmpty(branchName))
                request.AddQueryParameter("ref", branchName);

            return EnsureSuccess(() => Client.ExecuteTaskAsync<IEnumerable<Pipeline>>(request));
        }

        public async Task<Branch> GetBranch(string projectId, string branchName)
        {
            var r = await GetBranches(projectId, branchName : branchName);
            return r.FirstOrDefault();
        }

        public Task<IEnumerable<Branch>> GetBranches(string projectId, string branchName = "")
        {
            var request = new RestRequest("projects/{projectId}/repository/branches/{branchName}", Method.GET);
            request.AddUrlSegment("projectId", HttpUtility.UrlEncode(projectId));
            request.AddUrlSegment("branchName", branchName);

            return EnsureSuccess(() => Client.ExecuteTaskAsync<IEnumerable<Branch>>(request));
        }

        public async Task<Commit> GetCommitBySHA(string projectId, string commitSHA)
        {
            var r = await GetCommits(projectId, commitSHA : commitSHA);
            return r.FirstOrDefault();
        }

        public Task<IEnumerable<Commit>> GetCommits(string projectId, string commitSHA = "")
        {
            var request = new RestRequest("projects/{projectId}/repository/commits/{commitSHA}", Method.GET);
            request.AddUrlSegment("projectId", HttpUtility.UrlEncode(projectId));
            request.AddUrlSegment("commitSHA", commitSHA);

            return EnsureSuccess(() => Client.ExecuteTaskAsync<IEnumerable<Commit>>(request));
        }

        public Task<IEnumerable<Job>> GetJobs(string projectId, string pipelineId)
        {
            var request = new RestRequest("projects/{projectId}/pipelines/{pipelineId}/jobs", Method.GET);
            request.AddUrlSegment("projectId", HttpUtility.UrlEncode(projectId));
            request.AddUrlSegment("pipelineId", pipelineId);

            request.AddQueryParameter("per_page", "10000");//Arbitrary value

            return EnsureSuccess(() => Client.ExecuteTaskAsync<IEnumerable<Job>>(request));
        }

        public Task<IEnumerable<Branch>> SearchForBranchInProject(string projectId, string branchPartialName)
        {
            var request = new RestRequest("projects/{projectId}/repository/branches?search={branchPartialName}", Method.GET);
            request.AddUrlSegment("projectId", HttpUtility.UrlEncode(projectId));
            request.AddUrlSegment("branchPartialName", branchPartialName);

            request.AddQueryParameter("per_page", "10000");

            return EnsureSuccess(() => Client.ExecuteTaskAsync<IEnumerable<Branch>>(request));
        }

        private async Task<T> EnsureSuccess<T>(Func<Task<IRestResponse<T>>> requestTask)
        {
            var r = await requestTask();
            if (!r.IsSuccessful)
                throw new ApplicationHttpRequestException(r);

            return r.Data;
        }
    }
}
