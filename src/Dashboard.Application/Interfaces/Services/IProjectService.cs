using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Dashboard.Core.Entities;
using Newtonsoft.Json.Linq;

namespace Dashboard.Application.Interfaces.Services
{
    public interface IProjectService
    {
        Task<Project> GetProjectByIdAsync(int id);
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task DeleteProjectAsync(int id);
        Task<ServiceObjectResult<Project>> UpdateProjectAsync(Project updatedProject);
        Task<ServiceObjectResult<Project>> CreateProjectAsync(Project project);

        Task<IEnumerable<string>> SearchForBranchInProject(int projectId, string searchValue);
        Task UpdateCiDataForProjectAsync(int projectId);
        Task UpdateMissingBranch(int projectId, string branchName);

        Task<IEnumerable<Pipeline>> GetPipelinesForPanel(int panelID);

        void FireJobUpdate(string providerName, object body);
        void FirePipelineUpdate(string providerName, object body);
        //void FireProjectUpdate(string providerName, object body);

        Task WebhookJobUpdate(string providerName, object body);
        Task WebhookPipelineUpdate(string providerName, object body);
        //Task WebhookProjectUpdate(string providerName, object body);
    }
}