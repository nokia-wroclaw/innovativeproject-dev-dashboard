using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Core.Interfaces;

namespace Dashboard.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class WebhookController
    {
        //private readonly IProjectService _projectService;
        private readonly ICronJobsManager _cronJobsManager;

        public WebhookController(/*IProjectService projectService, */ICronJobsManager cronJobsManager)
        {
            //_projectService = projectService;
            _cronJobsManager = cronJobsManager;
        }

        [HttpPost]
        public async Task<IActionResult> GitlabUpdateForProject(int projectId)
        {
            //Task.Run(() => _projectService.UpdateCiDataForProjectAsync(projectId));
            //await _projectService.UpdateCiDataForProjectAsync(projectId);
            _cronJobsManager.FireProjectUpdate(projectId);
            return new OkResult();
        }
    }
}
