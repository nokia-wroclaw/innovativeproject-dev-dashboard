﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Core.Entities;
using Dashboard.WebApi.ApiModels;
using Dashboard.WebApi.ApiModels.Requests;
using Dashboard.WebApi.ApiModels.Responses;
using Dashboard.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.WebApi.Controllers
{
    [Route("api/[controller]/")]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET api/Project
        [HttpGet]
        public async Task<IEnumerable<ResponseProject>> Get()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            var responseProjects = projects.Select(p => new ResponseProject(p));
            return responseProjects;
        }

        // GET api/Project/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
                return NotFound();

            return Json(new ResponseProject(project));
        }

        // POST api/Project
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody]CreateProject model)
        {
            //TODO: change when automapper
            var project = new Project()
            {
                ProjectTitle = model.ProjectTitle,
                ApiAuthenticationToken = model.ApiAuthenticationToken,
                ApiHostUrl = model.ApiHostUrl,
                ApiProjectId = model.ApiProjectId,
                DataProviderName = model.DataProviderName,
                CiDataUpdateCronExpression = model.CiDataUpdateCronExpression,
                PipelinesNumber = model.PipelinesNumber == 0 ? 10 : model.PipelinesNumber
            };

            var r = await _projectService.CreateProjectAsync(project);
            return ApiResponse.FromServiceResult(r);
        }

        // PUT api/Project/5
        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Put(int id, [FromBody]UpdateProject model)
        {
            //TODO: change when automapper
            var updatedProject = new Project()
            {
                Id = id,
                ProjectTitle = model.ProjectTitle,
                ApiAuthenticationToken = model.ApiAuthenticationToken,
                ApiHostUrl = model.ApiHostUrl,
                ApiProjectId = model.ApiProjectId,
                DataProviderName = model.DataProviderName,
                CiDataUpdateCronExpression = model.CiDataUpdateCronExpression,
                PipelinesNumber = model.PipelineNumber
            };

            var r = await _projectService.UpdateProjectAsync(updatedProject);
            return ApiResponse.FromServiceResult(r);
        }

        // DELETE api/Project/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _projectService.DeleteProjectAsync(id);
        }
    }
}