using System.Collections.Generic;
using System.Threading.Tasks;
using Dashboard.Core.Entities;

namespace Dashboard.Core.Interfaces
{
    public interface ICiDataProvider
    {
        string Name { get; }
        Task<IEnumerable<Pipeline>> GetAllPipelines(string apiHost, string apiKey, string apiProjectId);
        Task<Pipeline> GetBranchPipeLine(string apiHost, string apiKey, string apiProjectId, string branchName);
        Task<IEnumerable<string>> GetAllProjectBranchNames(string apiHost, string apiKey, string apiProjectId);
        Task<Pipeline> GetSpecificPipeline(string apiHost, string apiKey, string apiProjectId, string pipeId);
    }
}
