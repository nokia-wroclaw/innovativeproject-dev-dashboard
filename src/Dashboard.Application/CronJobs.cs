using System.Linq;
using System.Threading.Tasks;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Core.Exceptions;
using Hangfire;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Dashboard.Application
{
    public static class CronJobs
    {
        public static void Register()
        {
            //Fetch Data Panels Project
            RecurringJob.AddOrUpdate<EnqueueFetchProjectsCiDataJob>("fetch-cidata-projects",
                j => j.EnqueueFetching(), EnqueueFetchProjectsCiDataJob.CronExpression);
        }
    }

    public class EnqueueFetchProjectsCiDataJob
    {
        public static readonly string CronExpression = "*/4 * * * *";

        private readonly IPanelService _panelService;

        public EnqueueFetchProjectsCiDataJob(IPanelService panelService)
        {
            _panelService = panelService;
        }

        public async Task EnqueueFetching()
        {
            var activeProjects = (await _panelService.GetActiveProjectIds()).ToList();

            activeProjects.ForEach(projectId =>
                BackgroundJob.Enqueue<SafeJob>(j => j.Fire(projectId)));
        }

        private class SafeJob
        {
            private readonly IProjectService _service;
            private readonly ILogger<EnqueueFetchProjectsCiDataJob> _logger;

            public SafeJob(IProjectService service, ILogger<EnqueueFetchProjectsCiDataJob> logger)
            {
                _service = service;
                _logger = logger;
            }

            public async Task Fire(int projectId)
            {
                try
                {
                    await _service.UpdateCiDataForProjectAsync(projectId);
                }
                catch (ApplicationHttpRequestException ex)
                {
                    _logger.LogWarning("Exception: ApplicationHttpRequestException in EnqueueFetchProjectsCiDataJob: " + JsonConvert.SerializeObject(new
                    {
                        Response = new
                        {
                            StatusCode = ex.Response.StatusCode,
                            StatusDescription = ex.Response.StatusDescription,
                            Content = ex.Response.Content,
                            IsSuccessful = ex.Response.IsSuccessful,
                            ResponseStatus = ex.Response.ResponseStatus,
                        }
                    }));
                }
            }
        }
    }
}