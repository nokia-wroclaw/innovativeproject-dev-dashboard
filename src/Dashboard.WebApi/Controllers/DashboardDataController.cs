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


        private readonly IProjectService _projectService;

        public DashboardDataController(IProjectService projectService, ICIDataProviderFactory ciDataProviderFactory)
        {
            _projectService = projectService;
            _ciDataProviderFactory = ciDataProviderFactory;
        }

        [HttpGet]
        public IEnumerable<string> SupportedProviders()
        {
            return _ciDataProviderFactory.GetSupportedProviders.Select(p => p.Name);
        }

        //api/DashboardData/Project/1
        [HttpGet("{projectId}")]
        public async Task<IActionResult> Project(int projectId)
        {


            var project = await _projectService.GetProjectById(projectId);
            if (project == null)
                return NotFound();

            return Json(project);
        }

    }
}