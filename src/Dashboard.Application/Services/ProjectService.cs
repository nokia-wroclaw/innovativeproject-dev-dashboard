using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Core.Entities;
using Dashboard.Core.Exceptions;
using Dashboard.Core.Interfaces;
using Dashboard.Core.Interfaces.Repositories;
using Hangfire;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using Dashboard.Application.Validators;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace Dashboard.Application.Services
{
    //TODO: add some validation
    public class ProjectService : IProjectService
    {
        private readonly ILogger<ProjectService> _logger;

        private readonly IPipelineRepository _pipelineRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ICiDataProviderFactory _ciDataProviderFactory;
        private readonly ICronJobsManager _cronJobsManager;
        private readonly IPanelRepository _panelRepository;

        public ProjectService(
            IPipelineRepository pipelineRepository,
            IProjectRepository projectRepository,
            ICiDataProviderFactory ciDataProviderFactory,
            ICronJobsManager cronJobsManager,
            IPanelRepository panelRepository,
            ILogger<ProjectService> logger)
        {
            _ciDataProviderFactory = ciDataProviderFactory;
            _cronJobsManager = cronJobsManager;
            _projectRepository = projectRepository;
            _pipelineRepository = pipelineRepository;
            _panelRepository = panelRepository;
            _logger = logger;
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

            _cronJobsManager.UnregisterUpdateCiDataForProject(entity.Id);

            _projectRepository.Delete(entity);
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
            project.CiDataUpdateCronExpression = updatedProject.CiDataUpdateCronExpression;

            _cronJobsManager.UpdateCiDataForProject(updatedProject);

            var r = await _projectRepository.UpdateAsync(project, updatedProject.Id);
            await _projectRepository.SaveAsync();

            return r;
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            var r = await _projectRepository.AddAsync(project);
            await _projectRepository.SaveAsync();

            _cronJobsManager.UpdateCiDataForProject(project);

            return r;
        }

        /// <summary>
        ///     Returns all branches (names) from web directly. Slow, but always updated and not so often used
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> SearchForBranchInProject(int projectId, string searchValue)
        {
            var project = await GetProjectByIdAsync(projectId);
            if (project == null) return null;

            //TODO: Refactor so this method returns error string and piplines, some validation, maybe move to CiDataService?
            var dataProvider = _ciDataProviderFactory.CreateForProviderName(project.DataProviderName);

            var r = await dataProvider.SearchBranchInProject(project.ApiHostUrl, project.ApiAuthenticationToken,
                project.ApiProjectId, searchValue);
            return r;
        }

        public async Task<IEnumerable<Pipeline>> UpdateLocalDatabase(int projectId, IEnumerable<string> staticPipes)
        {
            var project = await GetProjectByIdAsync(projectId);
            if (project == null)
                return null;
            var dataProvider = _ciDataProviderFactory.CreateForProviderName(project.DataProviderName);

            var targetPipelineNumber = project.PipelinesNumber - project.Pipelines.Count;

            var actualPipelineNumber = project.Pipelines.Count;
            var newPipelines = await dataProvider.GetLatestPipelines(project.ApiHostUrl, project.ApiAuthenticationToken, project.ApiProjectId, targetPipelineNumber, project.Pipelines, staticPipes);
            return newPipelines;
        }

        /// <summary>
        ///     Downloads data from CiDataProvider for given project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>All pipelines</returns>
        public async Task UpdateCiDataForProjectAsync(int projectId)
        {
            //await UpdateLocalDatabase(projectId);
            var project = await GetProjectByIdAsync(projectId);
            if (project == null) return;

            //TODO: Refactor so this method returns error string and piplines, some validation, maybe move to CiDataService?
            var dataProvider = _ciDataProviderFactory.CreateForProviderName(project.DataProviderName);

            var staticBranches = (await _panelRepository.FindAllAsync(p => p.Discriminator.Equals(nameof(StaticBranchPanel)))).Select(p => ((StaticBranchPanel)p).StaticBranchName);//await _staticBranchPanelRepository.GetBranchNamesFromStaticPanelsForProject(project.Id);
            var updatePiplineTasks = staticBranches.Select(b =>
                dataProvider.GetBranchPipeLine(project.ApiHostUrl, project.ApiAuthenticationToken, project.ApiProjectId, b));

            var updatedPipelines = (await Task.WhenAll(updatePiplineTasks));
            var updatedPipelinesSpecific = updatedPipelines.Select(p => dataProvider.GetSpecificPipeline(project.ApiHostUrl, project.ApiAuthenticationToken, project.ApiProjectId, p.DataProviderId.ToString()));

            //Fill with dynamic
            var downloadedPipelines = (await UpdateLocalDatabase(projectId, staticBranches));
            var downloadedPipelinesSpecific = downloadedPipelines.Select(p => dataProvider.GetSpecificPipeline(project.ApiHostUrl, project.ApiAuthenticationToken, project.ApiProjectId, p.DataProviderId.ToString()));

            //Merge
            var localPipelines = project.Pipelines;
            var output = localPipelines.Where(p => !staticBranches.Contains(p.Ref)).Select(p => p).ToList();
            output.AddRange(await Task.WhenAll(downloadedPipelinesSpecific));
            output.AddRange(await Task.WhenAll(updatedPipelinesSpecific));

            //Save update to DB
            int howManyToDelete = output.Count - project.PipelinesNumber >= 0 ? output.Count - project.PipelinesNumber : 0;
            _pipelineRepository.DeleteRange(output.Take(howManyToDelete));
            project.Pipelines = output.TakeLast(project.PipelinesNumber).ToList();

            await _projectRepository.UpdateAsync(project, project.Id);
            await _projectRepository.SaveAsync();

            _logger.LogInformation($"Updated cidata for project: {project.Id}");
        }
        
        public void FireProjectUpdate(string providerName, JObject body)
        {
            BackgroundJob.Enqueue<IProjectService>(s => s.WebhookFunction(providerName, body));
        }

        public async Task WebhookFunction(string providerName, JObject body)
        {
            var dataProvider = _ciDataProviderFactory.CreateForProviderLowercaseName(providerName.ToLower());
            string apiProjectId = dataProvider.GetProjectIdFromWebhookRequest(body);

            int projectId = (await _projectRepository.FindOneByAsync(p => p.DataProviderName.Equals(providerName, StringComparison.OrdinalIgnoreCase) && p.ApiProjectId.Equals(apiProjectId))).Id;
            await UpdateCiDataForProjectAsync(projectId);
        }

        public async Task<StaticAndDynamicPanelDTO> GetPipelinesForPanel(int panelID)
        {
            var panel = (await _panelRepository.GetByIdAsync(panelID));
            return await panel.GetPipelinesDTOForPanel(panelID, _projectRepository);
        }
    }
}