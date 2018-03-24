﻿using System;
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
            return await _panelService.GetAllPanelsAsync();
        }

        // GET api/Panel/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var panel = await _panelService.GetPanelByIdAsync(id);
            if (panel == null)
                return NotFound();

            return Json(panel);
        }

        // POST api/Panel
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody]CreatePanel model)
        {
            //TODO: change when automapper
            var panel = new Panel()
            {
                Title = model.Title,
                Position = new PanelPosition() { Column = model.Position.Column, Row = model.Position.Row },
                Data = model.Data,
                Type = model.Type,
                //Static branch name not required
                StaticBranchNames = model.StaticBranchNames
            };

            var created = await _panelService.CreatePanelAsync(panel, model.ProjectId);
            return Json(created);
        }

        // PUT api/Panel/5
        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Put(int id, [FromBody]UpdatePanel model)
        {
            //TODO: change when automapper
            var updatedPanel = new Panel()
            {
                Id = id,
                Title = model.Title,
                Data = model.Data,
                Type = model.Type,
                //Static branch name not required
                StaticBranchNames = model.StaticBranchNames,
                Position = new PanelPosition() { Column = model.Position.Column, Row = model.Position.Row }
            };

            var r = await _panelService.UpdatePanelAsync(updatedPanel, model.ProjectId);
            return Json(r);
        }

        // DELETE api/Panel/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _panelService.DeletePanelAsync(id);
        }

        // PUT api/Panel/5/Position
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
                Hight = model.Hight
            };

            var r = await _panelService.UpdatePanelPosition(id, newPosition);
            if (r == null) return NotFound();

            return Json(r);
        }
    }
}