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
    [Route("api/[controller]")]
    public class WebhookController : Controller
    {
        private readonly IProjectService _projectService;

        public WebhookController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<IActionResult> GitlabUpdateForProject()
        {
            //use in local to tests
            //int projectId = (await _projectService.GetProjectIdForWebhook(@"https://gitlab.com", Request.Body));

            //use in deploy
            //int projectId = (await _projectService.GetProjectIdForWebhook(Request.Host.ToUriComponent(), Request.Body));

            Stream stream = Request.Body;
            stream.Position = 0;
            StreamReader s = new StreamReader(stream);
            string bdy = s.ReadToEnd();
            

            _projectService.FireProjectUpdate(@"https://gitlab.com", bdy);

            return new OkResult();// Content(Request.Host.Host);
        }
    }
}
