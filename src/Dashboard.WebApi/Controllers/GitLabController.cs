using Dashboard.Application.Interfaces.Services;
using System.Threading.Tasks;
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
            return Ok(_gitLabDataProvider.GetMasterAsync());
        }

        ////GET api/gitlab/master
        //[Route("[action]")]
        //public async Task<IActionResult> Master()
        //{
        //    return Ok(_gitLabDataProvider.GetMasterAsync());
        //}
    }
}