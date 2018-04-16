using System.Collections.Generic;
using System.Threading.Tasks;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Core.Entities;
using Dashboard.WebApi.ApiModels.Requests;
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
        public async Task<IEnumerable<Project>> Get()
        {
            return await _projectService.GetAllProjectsAsync();
        }

        // GET api/Project/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
                return NotFound();

            return Json(project);
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
                CiDataUpdateCronExpression = model.CiDataUpdateCronExpression
            };

            var createdProject = await _projectService.CreateProjectAsync(project);
            return Json(createdProject);
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
                CiDataUpdateCronExpression = model.CiDataUpdateCronExpression
            };

            var r = await _projectService.UpdateProjectAsync(updatedProject);
            return Json(r);
        }

        // DELETE api/Project/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _projectService.DeleteProjectAsync(id);
        }
    }
}