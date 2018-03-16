using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.WebApi.Controllers
{
    //TODO: nazwa do zmiany
    [Route("api/[controller]")]
    public class DashboardDataController : Controller
    {
        private readonly ICIDataProviderFactory _ciDataProviderFactory;

        public DashboardDataController(ICIDataProviderFactory ciDataProviderFactory)
        {
            _ciDataProviderFactory = ciDataProviderFactory;
        }

        public IEnumerable<string> SupportedProviders()
        {
            return _ciDataProviderFactory.GetSupportedProviders.Select(p => p.Name);
        }

        //api/DashboardData/AllPipelines/GitLab
        [HttpGet("[action]/{providerName}")]
        public async Task<IActionResult> AllPipelines(string providerName)
        {
            var provider = _ciDataProviderFactory.CreateForProviderName(providerName);

            if (provider == null)
                return NoContent();

            var allPipelines = await provider.GetAllAsync();
            return Json(new
            {
                allPipelines
            });
        }

        [HttpGet("[action]/{providerName}")]
        public async Task<IActionResult> Master(string providerName)
        {
            var provider = _ciDataProviderFactory.CreateForProviderName(providerName);

            if (provider == null)
                return NoContent();

            var master = await provider.GetMasterAsync();
            return Json(new
            {
                master
            });
        }
    }
}