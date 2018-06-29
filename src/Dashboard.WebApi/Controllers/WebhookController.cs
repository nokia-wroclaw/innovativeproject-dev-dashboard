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
using GitLabModel = Dashboard.Application.GitLabApi.Models;
using Dashboard.Application.GitLabApi;
using System.Collections.Specialized;

namespace Dashboard.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class WebhookController : Controller
    {
        private readonly IProjectService _projectService;

        public WebhookController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        //api/Webhook/gitlab
        [HttpPost("{provider}")]
        public IActionResult JobWebhook(string provider, [FromBody] object body)
        {
            _projectService.FireJobUpdate(provider, body);

            return Ok();
        }

        //api/Webhook/gitlab
        [HttpPost("{provider}")]
        public IActionResult PipelineWebhook(string provider, [FromBody] object body)
        {
            _projectService.FirePipelineUpdate(provider, body);

            return Ok();
        }

        //api/Webhook/gitlab
        [HttpPost("{provider}")]
        [Consumes("application/x-www-form-urlencoded")]
        public IActionResult PipelineWebhook(string provider)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (var key in Request.Form.Keys)
            {
                var value = Request.Form[key];
                dict.Add(key, value);
            }

            _projectService.FirePipelineUpdate(provider, dict);

            return Ok();
        }
    }
}
