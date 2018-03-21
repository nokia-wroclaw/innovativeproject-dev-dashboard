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

        public async Task<Pipeline> GetPipeline(string projectId, string pipelineId)
        {
            var r = await GetPipelines(projectId, pipelineId);
            return r.FirstOrDefault();
        }

        public async Task<IEnumerable<Pipeline>> GetPipelines(string projectId, string pipelineId = "", string branchName = "")
        {
            var request = new RestRequest("projects/{projectId}/pipelines/{pipelineId}", Method.GET);
            request.AddUrlSegment("projectId", HttpUtility.UrlEncode(projectId));
            request.AddUrlSegment("pipelineId", pipelineId);

            if (!string.IsNullOrEmpty(branchName))
                request.AddQueryParameter("ref", branchName);

            var r = await Client.ExecuteTaskAsync<List<Pipeline>>(request);
            return r.Data;
        }
    }
}
