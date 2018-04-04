using System.Linq;
using System.Threading.Tasks;
using Dashboard.Application.Interfaces.Services;
using Hangfire;

namespace Dashboard.Application
{
    public static class CronJobs
    {
        public static void Register()
        {
            //Fetch Data Panels Project
            RecurringJob.AddOrUpdate<EnqueueFetchProjectsCiDataJob>("fetch-data-panels-project",
                j => j.EnqueueFetching(), EnqueueFetchProjectsCiDataJob.CronExpression);
        }
    }

    public class EnqueueFetchProjectsCiDataJob
    {
        public static readonly string CronExpression = "*/3 * * * *";

        private readonly IPanelService _panelService;

        public EnqueueFetchProjectsCiDataJob(IPanelService panelService)
        {
            _panelService = panelService;
        }

        public async Task EnqueueFetching()
        {
            var activeProjects = (await _panelService.GetActiveProjectIds()).ToList();

            activeProjects.ForEach(projectId =>
                BackgroundJob.Enqueue<IProjectService>(s => s.UpdateCiDataForProjectAsync(projectId)));
        }
    }
}