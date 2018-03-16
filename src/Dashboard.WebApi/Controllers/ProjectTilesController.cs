using System.Collections.Generic;
using System.Threading.Tasks;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Core.Entities;
using Dashboard.WebApi.ApiModels.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.WebApi.Controllers
{
    [Route("api/[controller]/")]
    public class ProjectTilesController : Controller
    {
        private readonly IProjectTileService _projectTileService;

        public ProjectTilesController(IProjectTileService projectTileService)
        {
            _projectTileService = projectTileService;
        }

        // GET api/ProjectTiles
        [HttpGet]
        public async Task<IEnumerable<ProjectTile>> Get()
        {
            return await _projectTileService.GetAllTilesAsync();
        }

        // GET api/ProjectTiles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tile = await _projectTileService.GetTileByIdAsync(id);
            if (tile == null)
                return NotFound();

            return Json(tile);
        }

        // POST api/ProjectTiles
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateProjectTile model)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            //TODO: change when automapper
            var tile = new ProjectTile
            {
                ApiAuthenticationToken = model.ApiAuthenticationToken,
                ApiHostUrl = model.ApiHostUrl,
                ApiProjectId = model.ApiProjectId,
                DataProviderName = model.DataProviderName,
                FrontConfig = model.FrontConfig
            };

            var createdTile = await _projectTileService.CreateTileAsync(tile);

            return Json(createdTile);
        }

        // PUT api/ProjectTiles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]UpdateProjectTile updatedProjectTile)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //TODO: change when automapper
            var updatedTile = new ProjectTile()
            {
                Id = id,
                ApiAuthenticationToken = updatedProjectTile.ApiAuthenticationToken,
                ApiHostUrl = updatedProjectTile.ApiHostUrl,
                ApiProjectId = updatedProjectTile.ApiProjectId,
                DataProviderName = updatedProjectTile.DataProviderName,
                FrontConfig = updatedProjectTile.FrontConfig
            };

            var r = await _projectTileService.UpdateTileAsync(updatedTile);
            return Json(r);
        }

        // DELETE api/ProjectTiles/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _projectTileService.DeleteTileAsync(id);
        }
    }
}