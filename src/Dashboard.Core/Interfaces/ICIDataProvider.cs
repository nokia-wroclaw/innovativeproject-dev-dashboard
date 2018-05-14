using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Dashboard.Core.Entities;
using Newtonsoft.Json.Linq;

namespace Dashboard.Core.Interfaces
{
    public interface ICiDataProvider
    {
        string Name { get; }

        Task<(IEnumerable<Pipeline> pipelines, int totalPages)> FetchNewestPipelines(string apiHost, string apiKey, string apiProjectId, int page, int perPage);

        /// <summary>
        /// Fetches specific pipeline with all info
        /// </summary>
        /// <param name="apiHost"></param>
        /// <param name="apiKey"></param>
        /// <param name="apiProjectId"></param>
        /// <param name="pipelineId"></param>
        /// <returns></returns>
        Task<Pipeline> FetchPipelineById(string apiHost, string apiKey, string apiProjectId, int pipelineId);


        /// <summary>
        /// Fetches newest pipeline for specific branch name
        /// </summary>
        /// <param name="apiHost"></param>
        /// <param name="apiKey"></param>
        /// <param name="apiProjectId"></param>
        /// <param name="branchName"></param>
        /// <returns></returns>
        Task<Pipeline> FetchPipeLineByBranch(string apiHost, string apiKey, string apiProjectId, string branchName);


        Task<IEnumerable<string>> SearchBranchInProject(string apiHost, string apiKey, string apiProjectId, string searchValue);

        string GetProjectIdFromWebhookRequest(object body);
    }
}
