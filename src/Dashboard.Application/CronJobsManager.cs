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
        }

        public void UpdateCiDataForProject(Project project)
        {
            RecurringJob.AddOrUpdate<CronFetchProjectCiDataJob>($"CronFetchProjectCiDataJob-{project.Id}", j => j.EnqueueFetching(project.Id), project.CiDataUpdateCronExpression);
        }
        public void UnregisterUpdateCiDataForProject(int projectId)
        {
            RecurringJob.RemoveIfExists($"CronFetchProjectCiDataJob-{projectId}");
        }

        public void FireProjectUpdate(int projectId)
        {
            //BackgroundJob.Enqueue( () => _projectService.UpdateCiDataForProjectAsync(projectId) );
            BackgroundJob.Enqueue<IProjectService>(s => s.UpdateCiDataForProjectAsync(projectId));
        }
    }
}
