using Dashboard.Application.Interfaces.Services;
using System.Threading.Tasks;
using Dashboard.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class GitLabController : Controller
    {
        private readonly ICIDataProvider _gitLabDataProvider;

        public GitLabController(ICIDataProvider gitLabDataProvider)
        {
            _gitLabDataProvider = gitLabDataProvider;
        }

        //GET api/gitlab
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _gitLabDataProvider.GetAllAsync());
        }

        //GET api/gitlab/master
        [Route("[action]")]
        public async Task<IActionResult> Master()
        {
            return Ok(await _gitLabDataProvider.GetMasterAsync());
        }
    }
}