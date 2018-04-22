using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Core.Interfaces;
using SimpleJson;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Dashboard.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class WebhookController : Controller
    {
        private readonly ICronJobsManager _cronJobsManager;
        private readonly IProjectService _projectService;

        public WebhookController(ICronJobsManager cronJobsManager, IProjectService projectService)
        {
            _cronJobsManager = cronJobsManager;
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<IActionResult> GitlabUpdateForProject()
        {
            //use in local to tests
            //int projectId = (await _projectService.GetProjectIdForWebhook(@"https://gitlab.com", Request.Body));
            //use in deploy
            int projectId = (await _projectService.GetProjectIdForWebhook(Request.Host.Host, Request.Body));
            _cronJobsManager.FireProjectUpdate(projectId);
            return Content(Request.Host.Host);
        }
    }
}
