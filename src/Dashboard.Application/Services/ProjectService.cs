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
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ICIDataProviderFactory _ciDataProviderFactory;

        public ProjectService(IProjectRepository projectRepository, ICIDataProviderFactory ciDataProviderFactory)
        {
            _ciDataProviderFactory = ciDataProviderFactory;
            _projectRepository = projectRepository;
        }

        public Task<Project> GetProjectById(int projectId)
        {
            return _projectRepository.GetByIdAsync(projectId);;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>All pipelines</returns>
        public async Task UpdatePipelinesForProjectAsync(int projectId)
        {
            //TODO: Refactor so this method returns error string and piplines, some validation
            var project = await GetProjectById(projectId);
            if (project == null) return;

            var dataProvider = _ciDataProviderFactory.CreateForProviderName(project.DataProviderName);
            var downloadedPiplines = await dataProvider.GetAllAsync(project.ApiHostUrl, project.ApiProjectId, project.ApiAuthenticationToken);

            project.Pipelines = project.Pipelines.Union(downloadedPiplines, new PipelineEqualityComparer());

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
