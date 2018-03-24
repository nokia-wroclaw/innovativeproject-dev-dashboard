using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Core.Entities;
using Dashboard.WebApi.ApiModels.Requests;
using Dashboard.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

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

        // POST api/Panel/CreateMemePanel
        [HttpPost("[action]")]
        [ValidateModel]
        public async Task<IActionResult> CreateMemePanel([FromBody]CreateMemePanel model)
        {
            //TODO: change when automapper
            var entity = new MemePanel()
            {
                Title = model.Title,
                Position = new PanelPosition() { Column = model.Position.Column, Row = model.Position.Row },
                MemeApiToken = model.MemeApiToken
            };

            var created = await _panelService.CreatePanelAsync(entity, model.ProjectId);
            return Json(created);
        }

        // POST api/Panel/CreateStaticBranchPanel
        [HttpPost("[action]")]
        [ValidateModel]
        public async Task<IActionResult> CreateStaticBranchPanel([FromBody]CreateStaticBranchPanel model)
        {
            //TODO: change when automapper
            var entity = new StaticBranchPanel()
            {
                Title = model.Title,
                Position = new PanelPosition() { Column = model.Position.Column, Row = model.Position.Row },
                StaticBranchNames = model.StaticBranchNames.Select(b => new BranchName() { Name = b })
            };

            var created = await _panelService.CreatePanelAsync(entity, model.ProjectId);
            return Json(created);
        }

        // POST api/Panel/CreateDynamicPipelinesPanel
        [HttpPost("[action]")]
        [ValidateModel]
        public async Task<IActionResult> CreateDynamicPipelinesPanel([FromBody]CreateDynamicPipelinesPanel model)
        {
            //TODO: change when automapper
            var entity = new DynamicPipelinesPanel()
            {
                Title = model.Title,
                Position = new PanelPosition() { Column = model.Position.Column, Row = model.Position.Row },
                HowManyLastPipelinesToRead = model.HowManyLastPipelinesToRead
            };

            var created = await _panelService.CreatePanelAsync(entity, model.ProjectId);
            return Json(created);
        }

        //// PUT api/Panel/5
        //[HttpPut("{id}")]
        //[ValidateModel]
        //public async Task<IActionResult> Put(int id, [FromBody]UpdatePanel model)
        //{
        //    ////TODO: change when automapper
        //    //var updatedPanel = new Panel()
        //    //{
        //    //    Id = id,
        //    //    Title = model.Title,
        //    //    Data = model.Data,
        //    //    Type = model.Type,
        //    //    //Static branch name not required
        //    //    StaticBranchNames = model.StaticBranchNames,
        //    //    Position = new PanelPosition() { Column = model.Position.Column, Row = model.Position.Row }
        //    //};

        //    //var r = await _panelService.UpdatePanelAsync(updatedPanel, model.ProjectId);
        //    //return Json(new PanelViewModel(r));

        //    return Ok();
        //}

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
            if (r == null) return NotFound();

            return Json(r);
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
                    Column = m.Column,
                    Row = m.Row,
                    Width = m.Width,
                    Height = m.Height
                };

                await _panelService.UpdatePanelPosition(m.PanelId, pos);
            });
        }
    }
}