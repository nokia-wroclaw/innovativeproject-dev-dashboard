using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Core.Interfaces;
using Dashboard.Core.Entities;

namespace Dashboard.WebApi.Controllers
{
    //TODO: nazwa do zmiany
    [Route("api/[controller]/[action]")]
    public class DashboardDataController : Controller
    {
        private readonly ICiDataProviderFactory _ciDataProviderFactory;
        private readonly IProjectService _projectService;

        public DashboardDataController(IProjectService projectService, ICiDataProviderFactory ciDataProviderFactory)
        {
            _projectService = projectService;
            _ciDataProviderFactory = ciDataProviderFactory;
        }

        [HttpGet]
        public IEnumerable<string> SupportedProviders()
        {
            return _ciDataProviderFactory.GetSupportedProviders.Select(p => p.Name);
        }

        //GET api/DashboardData/SearchForBranch?projectId=int&searchValue=string
        [HttpGet]
        public async Task<IEnumerable<string>> SearchForBranch(int projectId, string searchValue)
        {
            var result = await _projectService.SearchForBranchInProject(projectId, searchValue);
            return result;
        }

        [HttpGet]
        public async Task UpdateLocalDB(int projectId)
        {
            await _projectService.UpdateCiDataForProjectAsync(projectId);
        }

        [HttpGet]
        public async Task<StaticAndDynamicPanelDTO> PipelinesForPanel(int panelID)
        {
            return await _projectService.GetPipelinesForPanel(panelID);
        }
    }
}