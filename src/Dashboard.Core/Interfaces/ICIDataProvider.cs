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
        Task<IEnumerable<Pipeline>> GetAllPipelines(string apiHost, string apiKey, string apiProjectId);
        Task<Pipeline> GetBranchPipeLine(string apiHost, string apiKey, string apiProjectId, string branchName);
        Task<IEnumerable<string>> SearchBranchInProject(string apiHost, string apiKey, string apiProjectId, string searchValue);
        Task<Pipeline> GetSpecificPipeline(string apiHost, string apiKey, string apiProjectId, string pipeId);
        string GetProjectIdFromWebhookRequest(JObject body);

        //localPipes
        Task<IEnumerable<Pipeline>> GetLatestPipelines(string apiHost, string apiKey, string apiProjectId, int quantity, IEnumerable<Pipeline> localPipes, IEnumerable<string> staticPipes);
    }
}
