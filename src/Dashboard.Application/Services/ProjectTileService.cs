using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces;
using Dashboard.Core.Interfaces.Repositories;

namespace Dashboard.Application.Services
{
    public class ProjectTileService : IProjectTileService
    {
        private readonly IProjectTileRepository _projectTileRepository;
        private readonly ICIDataProviderFactory _ciDataProviderFactory;

        public ProjectTileService(IProjectTileRepository projectTileRepository, ICIDataProviderFactory ciDataProviderFactory)
        {
            _ciDataProviderFactory = ciDataProviderFactory;
            _projectTileRepository = projectTileRepository;
        }

        public Task<ProjectTile> GetTileById(int id)
        {
            return _projectTileRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>All pipelines</returns>
        public async Task UpdatePipelinesForProjectAsync(int projectId)
        {
            //TODO: Refactor so this method returns error string and piplines, some validation
            var project = await GetTileById(projectId);
            if (project == null) return;

            var dataProvider = _ciDataProviderFactory.CreateForProviderName(project.DataProviderName);

            var downloadedPiplines = await dataProvider.GetAllAsync(project.ApiHostUrl, project.ApiProjectId, project.ApiAuthenticationToken);

            //Join two lists, move to LinqExtensions ?
            var projectPipelines = project.Pipelines ?? new List<Pipeline>();
            project.Pipelines = projectPipelines.Concat(downloadedPiplines)
                .GroupBy(x => x.Id)
                .Select(g => g.First())
                .ToList();

            await _projectTileRepository.SaveAsync();
        }
    }

    public class PipelineEqualityComparer : IEqualityComparer<Pipeline>
    {
        public bool Equals(Pipeline x, Pipeline y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Pipeline obj)
        {
            return obj.ToString().ToLower().GetHashCode();
        }
    }
}
