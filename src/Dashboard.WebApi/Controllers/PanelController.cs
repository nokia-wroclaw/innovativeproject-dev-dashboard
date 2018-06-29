using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Core.Entities;
using Dashboard.WebApi.ApiModels;
using Dashboard.WebApi.ApiModels.Requests;
using Dashboard.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Examples;

namespace Dashboard.WebApi.Controllers
{
    [Route("api/[controller]/")]
    public class PanelController : Controller
    {
        private readonly IPanelService _panelService;

        public PanelController(IPanelService panelService)
        {
            _panelService = panelService;
        }

        // GET api/Panel
        [HttpGet]
        public async Task<IEnumerable<Panel>> Get()
        {
            var allPanels = await _panelService.GetAllPanelsAsync();
            return allPanels;
        }

        // GET api/Panel/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var panel = await _panelService.GetPanelByIdAsync(id);
            if (panel == null)
                return NotFound();

            //TODO: change when automapper
            return Json(panel);
        }

        // POST api/Panel/
        [HttpPost]
        [ValidateModel]
        [SwaggerRequestExample(typeof(CreatePanel), typeof(CreateDynamicPipelinePanelExample))]
        public async Task<IActionResult> Post([FromBody]CreatePanel model)
        {
            var entity = model.MapEntity(model);

            var createdResult = await _panelService.CreatePanelAsync(entity);
            return ApiResponse.FromServiceResult(createdResult);
        }

        // PUT api/Panel/5
        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Put(int id, [FromBody]UpdatePanel model)
        {
            var entity = model.MapEntity(model);
            entity.Id = id;

            var createdResult = await _panelService.UpdatePanelAsync(entity);
            return ApiResponse.FromServiceResult(createdResult);
        }

        // DELETE api/Panel/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _panelService.DeletePanelAsync(id);
        }

        // POST api/Panel/5/Position
        [HttpPost("{id}/[action]")]
        [ValidateModel]
        public async Task<IActionResult> Position(int id, [FromBody] UpdatePanelPosition model)
        {
            //TODO: change when automapper
            var newPosition = new PanelPosition()
            {
                Column = model.Column,
                Row = model.Row,
                Width = model.Width,
                Height = model.Height
            };

            var r = await _panelService.UpdatePanelPosition(id, newPosition);
            return ApiResponse.FromServiceResult(r);
        }

        // POST api/Panel/Positions
        [HttpPost("[action]")]
        [ValidateModel]
        public void Positions([FromBody] UpdatePanelPositions model)
        {
            //TODO: change when automapper
            model.UpdatedPanelPositions
            .ToList()
            .ForEach(async m =>
            {
                var pos = new PanelPosition()
                {
                    Column = m.Position.Column,
                    Row = m.Position.Row,
                    Width = m.Position.Width,
                    Height = m.Position.Height
                };

                await _panelService.UpdatePanelPosition(m.PanelId, pos);
            });
        }
    }
}