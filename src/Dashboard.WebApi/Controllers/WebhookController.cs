using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Core.Interfaces;
using SimpleJson;
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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

        //api/Webhook/gitlab
        [HttpPost("{provider}")]
        public async Task<IActionResult> Post(string provider, [FromBody] JObject body)
        {
            _projectService.FireProjectUpdate(provider, body);

            return new OkResult();
        }
    }
}
