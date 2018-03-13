using Dashboard.Application.Interfaces.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class GitLabController : Controller
    {
        private readonly IGitLabFetchService _gitLabFetchService;

        public GitLabController(IGitLabFetchService gitLabFetchService)
        {
            _gitLabFetchService = gitLabFetchService;
        }

        //GET api/gitlab
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var gitClient = NGitLab.GitLabClient.Connect("https://gitlab.com", "wL6jWfdAuqhqZ_MzERk1");
            var clientProjects = gitClient.Projects.Accessible;

            return Ok(clientProjects);
        }
    }
}