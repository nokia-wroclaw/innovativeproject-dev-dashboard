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
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ICIDataProviderFactory _ciDataProviderFactory;

        public ProjectService(IProjectRepository projectRepository, ICIDataProviderFactory ciDataProviderFactory)
        {
            _ciDataProviderFactory = ciDataProviderFactory;
            _projectRepository = projectRepository;
        }

        public Task<Project> GetProjectByIdAsync(int id)
        {
            return _projectRepository.GetByIdAsync(id);
        }

        public Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return _projectRepository.GetAllAsync();
        }

        public async Task DeleteProjectAsync(int id)
        {
            var project = await GetProjectByIdAsync(id);
            if(project == null)
                return;

            await _projectRepository.DeleteAsync(project);
            await _projectRepository.SaveAsync();
        }

        public async Task<Project> UpdateProjectAsync(Project updatedProject)
        {
            var project = await GetProjectByIdAsync(updatedProject.Id);
            if (project == null)
                return null;

            //TODO: change when automapper
            project.ApiAuthenticationToken = updatedProject.ApiAuthenticationToken;
            project.ApiHostUrl = updatedProject.ApiHostUrl;
            project.ApiProjectId = updatedProject.ApiProjectId;
            project.DataProviderName = updatedProject.DataProviderName;

            var r = await _projectRepository.UpdateAsync(project, updatedProject.Id);
            await _projectRepository.SaveAsync();

            return r;
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            var r = await _projectRepository.AddAsync(project);
            await _projectRepository.SaveAsync();

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
            var project = await GetProjectByIdAsync(projectId);
            if (project == null) return;

            var dataProvider = _ciDataProviderFactory.CreateForProviderName(project.DataProviderName);

            var downloadedPiplines = await dataProvider.GetAllAsync(project.ApiHostUrl, project.ApiProjectId, project.ApiAuthenticationToken);

            //Join two lists, move to LinqExtensions ?
            var projectPipelines = project.Pipelines ?? new List<Pipeline>();
            project.Pipelines = projectPipelines.Concat(downloadedPiplines)
                .GroupBy(x => x.Id)
                .Select(g => g.First())
                .ToList();

            await _projectRepository.SaveAsync();
        }
    }
}
