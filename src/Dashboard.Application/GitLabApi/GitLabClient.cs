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

        public Task<IEnumerable<Pipeline>> GetPipelines(string projectId, string pipelineId = "", int numberOfPipelines = 100, string branchName = "")
        {
            var request = new RestRequest("projects/{projectId}/pipelines/{pipelineId}", Method.GET);
            request.AddUrlSegment("projectId", HttpUtility.UrlEncode(projectId));
            request.AddUrlSegment("pipelineId", pipelineId);

            request.AddQueryParameter("per_page", numberOfPipelines.ToString());

            if (!string.IsNullOrEmpty(branchName))
                request.AddQueryParameter("ref", branchName);

            return Client.ExecuteTaskAsync<IEnumerable<Pipeline>>(request).EnsureSuccess();
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

            return Client.ExecuteTaskAsync<IEnumerable<Branch>>(request).EnsureSuccess();
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

            return Client.ExecuteTaskAsync<IEnumerable<Commit>>(request).EnsureSuccess();
        }

        //public Task<IEnumerable<Job>> GetJobs(string projectId, string pipelineId)
        //{
        //    var request = new RestRequest("projects/{projectId}/pipelines/{pipelineId}/jobs", Method.GET);
        //    request.AddUrlSegment("projectId", HttpUtility.UrlEncode(projectId));
        //    request.AddUrlSegment("pipelineId", pipelineId);

        //    request.AddQueryParameter("per_page", "10000");//Arbitrary value

        //    return Client.ExecuteTaskAsync<IEnumerable<Job>>(request).EnsureSuccess();
        //}

        public async Task<IEnumerable<Job>> GetJobs(string projectId, string pipelineId)
        {
            var request = new RestRequest("projects/{projectId}/pipelines/{pipelineId}/jobs", Method.GET);
            request.AddUrlSegment("projectId", HttpUtility.UrlEncode(projectId));
            request.AddUrlSegment("pipelineId", pipelineId);

            request.AddQueryParameter("per_page", "100");
            request.AddQueryParameter("page", "1");

            var response = await Client.ExecuteTaskAsync<List<Job>>(request);
            var consolidatedData = response.Data;
            int totalPages = -1;
            if (!int.TryParse(response.Headers.FirstOrDefault(p => p.Name == "X-Total-Pages").Value.ToString(), out totalPages))
                throw new InvalidCastException("Bad conversion of X-Total-Pages in GitlabClient.cs");

            List<Task<IRestResponse<List<Job>>>> nextResponses = new List<Task<IRestResponse<List<Job>>>>();
            for (int i = 2; i <= totalPages; i++)
            {
                var req = new RestRequest("projects/{projectId}/pipelines/{pipelineId}/jobs", Method.GET);
                req.AddUrlSegment("projectId", HttpUtility.UrlEncode(projectId));
                req.AddQueryParameter("per_page", "100");
                req.AddQueryParameter("page", i.ToString());

                nextResponses.Add(Client.ExecuteTaskAsync<List<Job>>(req));
            }
            var res = (await Task.WhenAll(nextResponses));
            foreach (var partialData in res)
                consolidatedData.AddRange(partialData.Data);

            return consolidatedData;
        }

        public Task<IEnumerable<Branch>> SearchForBranchInProject(string projectId, string branchPartialName)
        {
            var request = new RestRequest("projects/{projectId}/repository/branches?search={branchPartialName}", Method.GET);
            request.AddUrlSegment("projectId", HttpUtility.UrlEncode(projectId));
            request.AddUrlSegment("branchPartialName", branchPartialName);

            request.AddQueryParameter("per_page", "10000");

            return Client.ExecuteTaskAsync<IEnumerable<Branch>>(request).EnsureSuccess();
        }

        /// <summary>
        /// Return newest pipelines NOT FULL INFO, used to fullfill existing ones
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="howMany"></param>
        /// <param name="staticPipes"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Pipeline>> PickNewestPipelinesExcludingSome(string projectId, int howMany, IEnumerable<string> staticPipes)
        {
            List<Pipeline> downloadedPipelines = new List<Pipeline>();
            int pageCounter = 1;
            int totalPages = 1;
            while (downloadedPipelines.Count < howMany && pageCounter <= totalPages)
            {
                //Make request
                var request = new RestRequest("projects/{projectId}/pipelines", Method.GET);
                request.AddUrlSegment("projectId", HttpUtility.UrlEncode(projectId));

                request.AddQueryParameter("per_page", "100");
                request.AddQueryParameter("page", pageCounter.ToString());

                var response = await Client.ExecuteTaskAsync<List<Pipeline>>(request);
                if (!int.TryParse(response.Headers.FirstOrDefault(p => p.Name == "X-Total-Pages").Value.ToString(), out totalPages))
                    throw new InvalidCastException("Bad conversion of X-Total-Pages in GitlabClient.cs");

                //Process data
                downloadedPipelines.AddRange(response.Data.Where(p => !staticPipes.Contains(p.Ref)).Select(p => p));
                pageCounter++;
            }

            return downloadedPipelines.Take(howMany);
        }
    }
}
