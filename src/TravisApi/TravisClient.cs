using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using RestSharp;
using TravisApi.Models;
using TravisApi.Models.Responses;

namespace TravisApi
{
    /// <summary>
    /// https://developer.travis-ci.com/explore/#explorer
    /// </summary>
    public class TravisClient
    {
        private RestClient Client { get; }

        public TravisClient(string apiHost, string apiKey)
        {
            // Convert snake_casing to CamelCasing
            SimpleJson.SimpleJson.CurrentJsonSerializerStrategy = new SnakeJsonSerializerStrategy();

            Client = new RestClient
            {
                Authenticator = new ApiTokenAuthenticator(apiKey),
                BaseUrl = new Uri($"{apiHost}/")
            };
            Client.AddDefaultHeader("Travis-API-Version", "3");
            Client.AddDefaultHeader("Accept", "application/vnd.travis-ci.2.1+json");
        }

        public Task<Build> FetchBuildById(int buildId, bool includeJobs)
        {
            var request = new RestRequest("build/{buildId}", Method.GET);
            request.AddUrlSegment("buildId", buildId);

            if (includeJobs)
                request.AddQueryParameter("include", "build.jobs");

            return Client.ExecuteTaskAsync<Build>(request).EnsureSuccess();
        }

        public Task<GetBranchResponse> GetBranch(string projectId, string branch)
        {
            var request = new RestRequest("repo/{projectId}/branch/{branchName}", Method.GET);

            request.AddUrlSegment("projectId", projectId);
            request.AddUrlSegment("branchName", branch);

            return Client.ExecuteTaskAsync<GetBranchResponse>(request).EnsureSuccess();
        }

        public async Task<(IEnumerable<Build> builds, int totalPages)> GetNewestBuilds(string projectId, int page, int perPage, bool includeJobs)
        {
            var request = new RestRequest("repo/{projectId}/builds", Method.GET);
            request.AddUrlSegment("projectId", projectId);

            request.AddQueryParameter("limit", perPage.ToString());
            request.AddQueryParameter("offset", (page * perPage).ToString());

            if (includeJobs)
                request.AddQueryParameter("include", "build.jobs");

            var response = await Client.ExecuteTaskAsync<GetRepoBuildsResponse>(request).EnsureSuccess();


            return (response.Builds, response.Pagination.Count);
        }

        public Task<GetJobsResponse> GetJobs(int buildId)
        {
            var request = new RestRequest("build/{buildId}/jobs", Method.GET);
            request.AddUrlSegment("buildId", buildId);

            return Client.ExecuteTaskAsync<GetJobsResponse>(request).EnsureSuccess();
        }

    }
}
