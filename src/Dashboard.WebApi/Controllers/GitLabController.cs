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
            return Ok(_gitLabFetchService.GetAllAccessibleProjects());
        }
    }
}