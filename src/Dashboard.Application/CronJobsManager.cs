using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Application.CronJobs;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces;
using Hangfire;

namespace Dashboard.Application
{
    public class CronJobsManager : ICronJobsManager
    {
        private readonly IPanelService _panelService;

        public CronJobsManager(IPanelService panelService)
        {
            _panelService = panelService;
        }

        public void RegisterAllCronJobs()
        {
            var activeProjects = _panelService.GetActiveProjects().Result.ToList();

            activeProjects.ForEach(p => BackgroundJob.Enqueue(() => UpdateCiDataForProject(p)));

            RecurringJob.AddOrUpdate<CronRefreshMemePanelsImage>(nameof(CronRefreshMemePanelsImage), j => j.PerformRefresh(), "*/5 * * * *"); //Every 5 minutes
            RecurringJob.AddOrUpdate<CronScrapAndRotateMemeImages>(nameof(CronScrapAndRotateMemeImages), j => j.PerformWork("memes", 50), "0 0 * * *"); //Every day at midnight
        }

        public void UpdateCiDataForProject(Project project)
        {
            RecurringJob.AddOrUpdate<CronFetchProjectCiDataJob>($"CronFetchProjectCiDataJob-{project.Id}", j => j.EnqueueFetching(project.Id), project.CiDataUpdateCronExpression);
        }
        public void UnregisterUpdateCiDataForProject(int projectId)
        {
            RecurringJob.RemoveIfExists($"CronFetchProjectCiDataJob-{projectId}");
        }
    }
}
