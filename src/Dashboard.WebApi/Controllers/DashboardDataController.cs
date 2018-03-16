using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.WebApi.Controllers
{
    //TODO: nazwa do zmiany
    [Route("api/[controller]/[action]")]
    public class DashboardDataController : Controller
    {
        private readonly ICIDataProviderFactory _ciDataProviderFactory;
        private readonly IProjectTileService _projectTileService;

        public DashboardDataController(IProjectTileService projectTileService, ICIDataProviderFactory ciDataProviderFactory)
        {
            _projectTileService = projectTileService;
            _ciDataProviderFactory = ciDataProviderFactory;
        }

        [HttpGet]
        public IEnumerable<string> SupportedProviders()
        {
            return _ciDataProviderFactory.GetSupportedProviders.Select(p => p.Name);
        }

        //api/DashboardData/ProjectTile/1
        [HttpGet("{projectTileId}")]
        public async Task<IActionResult> ProjectTile(int projectTileId)
        {

            var project = await _projectTileService.GetTileById(projectTileId);
            if (project == null)
                return NotFound();

            return Json(project);
        }

        //api/DashboardData/UpdatePipelinesProjectTile
        [HttpPost("{projectTileId}")]
        public async Task<IActionResult> UpdatePipelinesProjectTile(int projectTileId)
        {
            var project = await _projectTileService.GetTileById(projectTileId);
            if (project == null)
                return NotFound();

            await _projectTileService.UpdatePipelinesForProjectAsync(projectTileId);

            return Ok();
        }
    }
}