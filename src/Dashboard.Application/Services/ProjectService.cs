using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Application.GitLabApi;
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
        private readonly IPanelRepository _panelRepository;
        private readonly ICiDataProviderFactory _ciDataProviderFactory;

        public ProjectService(IProjectRepository projectRepository, IPanelRepository panelRepository, ICiDataProviderFactory ciDataProviderFactory)
        {
            _ciDataProviderFactory = ciDataProviderFactory;
            _panelRepository = panelRepository;
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
            var entity = await GetProjectByIdAsync(id);
            if(entity == null)
                return;

            await _projectRepository.DeleteAsync(entity);
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
        /// Returns all branches (names) from web directly. Slow, but always updated and not so often used
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetAllProjectBranchNames(int projectId)
        {
            var project = await GetProjectByIdAsync(projectId);
            if (project == null) return null;

            //TODO: Refactor so this method returns error string and piplines, some validation, maybe move to CiDataService?
            var dataProvider = _ciDataProviderFactory.CreateForProviderName(project.DataProviderName);

            var r = await dataProvider.GetAllProjectBranchNames(project.ApiHostUrl, project.ApiAuthenticationToken, project.ApiProjectId);
            return r;
        }

        /// <summary>
        /// Downloads data from CiDataProvider for given project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>All pipelines</returns>
        public async Task UpdateCiDataForProjectAsync(int projectId)
        {
            var project = await GetProjectByIdAsync(projectId);
            if (project == null) return;

            //TODO: Refactor so this method returns error string and piplines, some validation, maybe move to CiDataService?
            var dataProvider = _ciDataProviderFactory.CreateForProviderName(project.DataProviderName);

            var downloadedPipelines = await dataProvider.GetAllPipelines(project.ApiHostUrl, project.ApiAuthenticationToken, project.ApiProjectId);
            //Get list of static branches
            var staticPanels = await _panelRepository.FindByAsync(p => (p.StaticBranchName != "") && (p.StaticBranchName != null));
            List<string> staticBranches = new List<string>();
            foreach (var panel in staticPanels)
                staticBranches.Add(panel.StaticBranchName);

            //Update with static pipelines
            List<Pipeline> updatedPipelines = new List<Pipeline>();
            foreach (var item in staticBranches)
            {
                updatedPipelines.Add(await dataProvider.GetBranchPipeLine(project.ApiHostUrl, project.ApiAuthenticationToken, project.ApiProjectId, item));
            }

            //Fill with newest pipelines
            foreach (var item in downloadedPipelines)
            {
                if (!staticBranches.Contains(item.Ref))
                    updatedPipelines.Add(new Pipeline { Id = item.Id, Ref = item.Ref, Sha = item.Sha, Status = item.Status });
                //Arbitrary pipelines count
                if (updatedPipelines.Count >= 10)
                    break;
            }
            //Save updated pipelines
            project.Pipelines = updatedPipelines;

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
