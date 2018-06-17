using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Core.Interfaces;
using Dashboard.Core.Entities;
using Dashboard.WebApi.ApiModels.Responses;
using Dashboard.Core.Interfaces.Repositories;

namespace Dashboard.WebApi.Controllers
{
    //TODO: nazwa do zmiany
    [Route("api/[controller]/[action]")]
    public class DashboardDataController : Controller
    {
        private readonly ICiDataProviderFactory _ciDataProviderFactory;
        private readonly IProjectService _projectService;
        private readonly IPanelRepository _panelRepository;

        public static bool IsUpdating { get; set; } = false;

        public DashboardDataController(IProjectService projectService, ICiDataProviderFactory ciDataProviderFactory, IPanelRepository panelRepository)
        {
            _projectService = projectService;
            _ciDataProviderFactory = ciDataProviderFactory;
            _panelRepository = panelRepository;
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
        public async Task<IEnumerable<ResponsePipeline>> PipelinesForPanel(int panelID)
        {
            var pipelinesForPanel = await _projectService.GetPipelinesForPanel(panelID);
            var panel = await _panelRepository.FindOneByAsync(p => p.Id == panelID);

            if(pipelinesForPanel.Contains(null) && panel.Discriminator.Equals(nameof(StaticBranchPanel)) && !DashboardDataController.IsUpdating)
            {
                DashboardDataController.IsUpdating = true;
                await _projectService.UpdateMissingBranch(panel.ProjectId.Value, ((StaticBranchPanel)panel).StaticBranchName);
                DashboardDataController.IsUpdating = false;
            }
            var returnPipelines = pipelinesForPanel.Select(p => p == null ? null : new ResponsePipeline(p));
            return returnPipelines;
        }
    }
}