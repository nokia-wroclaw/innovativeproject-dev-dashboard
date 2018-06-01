using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Task<Build> GetBuildById(int buildId, bool includeJobs, bool includeStages)
        {
            var request = new RestRequest("build/{buildId}", Method.GET);
            request.AddUrlSegment("buildId", buildId);

            var includeQueryParams = new List<string>();
            if (includeJobs) includeQueryParams.Add("build.jobs");
            if (includeStages) includeQueryParams.Add("build.stages");
            if(includeQueryParams.Any())
                request.AddQueryParameter("include", string.Join(",", includeQueryParams));

            return Client.ExecuteTaskAsync<Build>(request).EnsureSuccess();
        }

        public Task<GetBranchResponse> GetBranch(string projectId, string branch)
        {
            var request = new RestRequest("repo/{projectId}/branch/{branchName}", Method.GET);

            request.AddUrlSegment("projectId", projectId);
            request.AddUrlSegment("branchName", branch);

            return Client.ExecuteTaskAsync<GetBranchResponse>(request).EnsureSuccess();
        }

        public async Task<(IEnumerable<Build> builds, int totalPages)> GetNewestBuilds(string projectId, int page, int perPage, bool includeJobs, bool includeStages)
        {
            var request = new RestRequest("repo/{projectId}/builds", Method.GET);
            request.AddUrlSegment("projectId", projectId);

            request.AddQueryParameter("limit", perPage.ToString());
            request.AddQueryParameter("offset", (page * perPage).ToString());

            var includeQueryParams = new List<string>();
            if (includeJobs) includeQueryParams.Add("build.jobs");
            if (includeStages) includeQueryParams.Add("build.stages");
            if (includeQueryParams.Any())
                request.AddQueryParameter("include", string.Join(",", includeQueryParams));

            var response = await Client.ExecuteTaskAsync<GetRepoBuildsResponse>(request).EnsureSuccess();

            return (response.Builds, response.Pagination.Count);
        }

        public Task<GetUserResponse> GetUser()
        {
            var request = new RestRequest("user", Method.GET);

            return Client.ExecuteTaskAsync<GetUserResponse>(request).EnsureSuccess();
        }
    }
}
