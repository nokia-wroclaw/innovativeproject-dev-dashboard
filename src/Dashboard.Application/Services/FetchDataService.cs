using Dashboard.Application.GitLabApi.Models;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Core.Interfaces;
using Dashboard.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Application.Services
{
    public class FetchDataService : IFetchDataService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ICiDataProviderFactory _ciDataProviderFactory;

        public FetchDataService(IProjectRepository projectRepository, ICiDataProviderFactory ciDataProviderFactory)
        {
            _ciDataProviderFactory = ciDataProviderFactory;
            _projectRepository = projectRepository;
        }

        /// <summary>
        /// Downloads data from CiDataProvider for given project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>All pipelines</returns>
        public async Task UpdateCiDataForProjectAsync(int projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null) return;

            //TODO: Refactor so this method returns error string and piplines, some validation, maybe move to CiDataService?
            var dataProvider = _ciDataProviderFactory.CreateForProviderName(project.DataProviderName);

            var downloadedPiplines = await dataProvider.GetAllPipelines(project.ApiHostUrl, project.ApiAuthenticationToken, project.ApiProjectId);

            ////Join two lists, move to LinqExtensions ?
            //var projectPipelines = project.Pipelines ?? new List<Pipeline>();
            //project.Pipelines = projectPipelines.Concat(downloadedPiplines)
            //    .GroupBy(x => x.Id)
            //    .Select(g => g.First())
            //    .ToList();

            await _projectRepository.SaveAsync();
        }
    }
}
