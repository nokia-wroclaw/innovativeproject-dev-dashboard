using System.Threading.Tasks;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Core.Interfaces.Repositories;
using Hangfire;

namespace Dashboard.Application.CronJobs
{
    public class CronFetchProjectCiDataJob
    {
        private readonly IProjectRepository _projectRepository;

        public CronFetchProjectCiDataJob(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task EnqueueFetching(int projectId)
        {
            var dbProject = await _projectRepository.GetByIdAsync(projectId);

            BackgroundJob.Enqueue<IProjectService>(s => s.UpdateCiDataForProjectAsync(dbProject.Id));
        }
    }
}
