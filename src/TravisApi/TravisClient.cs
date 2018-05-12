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

        public Task<GetRepoBuildsResponse> FetchBuilds(string projectId, int? limit)
        {
            var request = new RestRequest("repo/{projectId}/builds/", Method.GET);
            request.AddUrlSegment("projectId", HttpUtility.UrlEncode(projectId));

            if (limit.HasValue)
                request.AddQueryParameter("limit", limit.ToString());

            return Client.ExecuteTaskAsync<GetRepoBuildsResponse>(request).EnsureSuccess();
        }


        public Task<Build> FetchBuildById(string projectId, int buildId)
        {
            var request = new RestRequest("build/{buildId}", Method.GET);
            //request.AddUrlSegment("projectId", HttpUtility.UrlEncode(projectId));
            request.AddUrlSegment("buildId", buildId);

            return Client.ExecuteTaskAsync<Build>(request).EnsureSuccess();
        }

        public Task<GetBranchResponse> GetBranch(string projectId, string branch)
        {
            var request = new RestRequest("repo/{projectId}/branch/{branchName}", Method.GET);
            request.AddUrlSegment("projectId", HttpUtility.UrlEncode(projectId));
            request.AddUrlSegment("branchName", branch);

            return Client.ExecuteTaskAsync<GetBranchResponse>(request).EnsureSuccess();
        }

        public Task<GetRepoBranches> GetBranches(string projectId, int? limit, bool? existsOnGithub)
        {
            var request = new RestRequest("repo/{projectId}/branches", Method.GET);
            request.AddUrlSegment("projectId", HttpUtility.UrlEncode(projectId));

            if (limit.HasValue)
                request.AddQueryParameter("limit", limit.ToString());

            if (existsOnGithub.HasValue)
                request.AddQueryParameter("exists_on_github", existsOnGithub.ToString());

            return Client.ExecuteTaskAsync<GetRepoBranches>(request).EnsureSuccess();
        }

        public async Task<(IEnumerable<Build> builds, int totalPages)> GetNewestBuilds(string projectId, int page, int perPage)
        {
            var request = new RestRequest("repo/{projectId}/builds", Method.GET);
            request.AddUrlSegment("projectId", HttpUtility.UrlEncode(projectId));

            request.AddQueryParameter("limit", perPage.ToString());
            request.AddQueryParameter("offset", (page * perPage).ToString());

            var response = await Client.ExecuteTaskAsync<GetRepoBuildsResponse>(request);


            return (response.Data.Builds, response.Data.Pagination.Count);
        }

    }
}
