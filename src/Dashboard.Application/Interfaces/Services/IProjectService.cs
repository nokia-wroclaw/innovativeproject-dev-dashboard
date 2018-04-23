using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Dashboard.Core.Entities;

namespace Dashboard.Application.Interfaces.Services
{
    public interface IProjectService
    {
        Task<Project> GetProjectByIdAsync(int id);
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task DeleteProjectAsync(int id);
        Task<Project> UpdateProjectAsync(Project updatedProject);
        Task<Project> CreateProjectAsync(Project project);

        Task<IEnumerable<string>> SearchForBranchInProject(int projectId, string searchValue);
        Task UpdateCiDataForProjectAsync(int projectId);

        Task<int> GetProjectIdForWebhook(string providerName, Stream body);
        void FireProjectUpdate(string providerName, string body);
        Task WebhookFunction(string providerName, string body);
    }
}
