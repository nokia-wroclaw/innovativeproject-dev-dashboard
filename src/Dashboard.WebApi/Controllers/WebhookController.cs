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

        ////api/Webhook/gitlab
        //[HttpPost("{provider}")]
        //public IActionResult ProjectWebhook(string provider, [FromBody] object body)
        //{
        //    _projectService.FireProjectUpdate(provider, body);

        //    return Ok();
        //}
    }
}
