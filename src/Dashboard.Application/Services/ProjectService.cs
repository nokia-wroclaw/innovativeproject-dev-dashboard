using System.Collections.Generic;
using System.Linq;
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
        private readonly IPanelRepository _panelRepository;
        private readonly IStaticBranchPanelRepository _staticBranchPanelRepository;
        private readonly ICiDataProviderFactory _ciDataProviderFactory;

        public ProjectService(
            IProjectRepository projectRepository, 
            IPanelRepository panelRepository,
            IStaticBranchPanelRepository staticBranchPanelRepository,
            ICiDataProviderFactory ciDataProviderFactory)
        {
            _ciDataProviderFactory = ciDataProviderFactory;
            _panelRepository = panelRepository;
            _staticBranchPanelRepository = staticBranchPanelRepository;
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
            if (entity == null)
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
        public async Task<IEnumerable<string>> SearchForBranchInProject(int projectId, string searchValue)
        {
            var project = await GetProjectByIdAsync(projectId);
            if (project == null) return null;

            //TODO: Refactor so this method returns error string and piplines, some validation, maybe move to CiDataService?
            var dataProvider = _ciDataProviderFactory.CreateForProviderName(project.DataProviderName);

            var r = await dataProvider.SearchBranchInProject(project.ApiHostUrl, project.ApiAuthenticationToken, project.ApiProjectId, searchValue);
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

            var staticBranches = await _staticBranchPanelRepository.GetBranchNamesFromStaticPanelsForProject(project.Id);

            //Create tasks
            var updatePiplineTasks = staticBranches
                                    .Select(b => dataProvider.GetBranchPipeLine(
                                        apiHost: project.ApiHostUrl,
                                        apiKey: project.ApiAuthenticationToken,
                                        apiProjectId: project.ApiProjectId,
                                        branchName: b)
                                    );

            //Await all results async
            var updatedPipelines = (await Task.WhenAll(updatePiplineTasks)).ToList();

            //Apparently faster way than LINQ, merge collections, discard duplicates
            var dict = updatedPipelines.ToDictionary(p => p.Sha);
            foreach (var pipe in downloadedPipelines)
            {
                dict[pipe.Sha] = pipe;
            }
            updatedPipelines = dict.Values.Take(11 - updatedPipelines.Count).ToList();

            var updatedPipesWithFullInfoTasks = updatedPipelines
                                            .Select(p => dataProvider.GetSpecificPipeline(
                                                project.ApiHostUrl, 
                                                project.ApiAuthenticationToken, 
                                                project.ApiProjectId, 
                                                p.DataProviderId.ToString())
                                            );
            var updatedPipesWithFullInfo = (await Task.WhenAll(updatedPipesWithFullInfoTasks)).ToList();


            project.Pipelines = updatedPipesWithFullInfo;

            await _projectRepository.UpdateAsync(project, project.Id);

            await _projectRepository.SaveAsync();
        }
    }
}
