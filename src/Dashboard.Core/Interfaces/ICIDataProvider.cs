using System.Collections.Generic;
using System.Threading.Tasks;
using Dashboard.Core.Entities;

namespace Dashboard.Core.Interfaces
{
    public interface ICIDataProvider
    {
        string Name { get; }
        Task<Pipeline> GetMasterAsync();
        Task<IEnumerable<Pipeline>> GetAllAsync(string host, string projectId, string apiKey);
    }
}
