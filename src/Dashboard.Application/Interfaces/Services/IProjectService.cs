using System.Collections.Generic;
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

        Task UpdateCiDataForProjectAsync(int projectId);
    }
}
