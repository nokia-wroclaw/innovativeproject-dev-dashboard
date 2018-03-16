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
    //TODO: add some validation
    public class ProjectTileService : IProjectTileService
    {
        private readonly IProjectTileRepository _projectTileRepository;
        private readonly ICIDataProviderFactory _ciDataProviderFactory;

        public ProjectTileService(IProjectTileRepository projectTileRepository, ICIDataProviderFactory ciDataProviderFactory)
        {
            _ciDataProviderFactory = ciDataProviderFactory;
            _projectTileRepository = projectTileRepository;
        }

        public Task<ProjectTile> GetTileByIdAsync(int id)
        {
            return _projectTileRepository.GetByIdAsync(id);
        }

        public Task<IEnumerable<ProjectTile>> GetAllTilesAsync()
        {
            return _projectTileRepository.GetAllAsync();
        }

        public async Task DeleteTileAsync(int id)
        {
            var tile = await GetTileByIdAsync(id);
            if(tile == null)
                return;

            await _projectTileRepository.DeleteAsync(tile);
            await _projectTileRepository.SaveAsync();
        }

        public async Task<ProjectTile> UpdateTileAsync(ProjectTile updatedTile)
        {
            var tile = await GetTileByIdAsync(updatedTile.Id);
            if (tile == null)
                return null;

            //TODO: change when automapper
            tile.ApiAuthenticationToken = updatedTile.ApiAuthenticationToken;
            tile.ApiHostUrl = updatedTile.ApiHostUrl;
            tile.ApiProjectId = updatedTile.ApiProjectId;
            tile.DataProviderName = updatedTile.DataProviderName;
            tile.FrontConfig = updatedTile.FrontConfig;

            var r = await _projectTileRepository.UpdateAsync(tile, updatedTile.Id);
            await _projectTileRepository.SaveAsync();

            return r;
        }

        public async Task<ProjectTile> CreateTileAsync(ProjectTile tile)
        {
            var r = await _projectTileRepository.AddAsync(tile);
            await _projectTileRepository.SaveAsync();

            return r;
        }

        /// <summary>
        /// Downloads pipelines from CiDataProvider for given project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>All pipelines</returns>
        public async Task UpdatePipelinesForProjectAsync(int projectId)
        {
            //TODO: Refactor so this method returns error string and piplines, some validation
            var project = await GetTileByIdAsync(projectId);
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
