using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Core.Entities;
using Dashboard.Core;
using Dashboard.Core.Interfaces;
using Dashboard.Core.Interfaces.Repositories;
using Hangfire;
using Microsoft.Extensions.Logging;
using System;
using Dashboard.Application.Validators;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Dashboard.Core.Interfaces.WebhookProviders;

namespace Dashboard.Application.Services
{
    //TODO: add some validation
    public class ProjectService : IProjectService
    {
        private readonly ILogger<ProjectService> _logger;

        private readonly IPipelineRepository _pipelineRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IValidationService _validationService;
        private readonly ICiDataProviderFactory _ciDataProviderFactory;
        private readonly ICronJobsManager _cronJobsManager;
        private readonly IPanelRepository _panelRepository;
        private readonly IStaticBranchPanelRepository _staticBranchPanelRepository;
        private readonly IConfiguration _configuration;
        private readonly IStageRepository _stageRepository;
        private readonly IJobRepository _jobRepository;

        public ProjectService(
            IPipelineRepository pipelineRepository,
            IProjectRepository projectRepository,
            IValidationService validationService,
            ICiDataProviderFactory ciDataProviderFactory,
            ICronJobsManager cronJobsManager,
            IPanelRepository panelRepository,
            IStaticBranchPanelRepository staticBranchPanelRepository,
            IConfiguration configuration,
            ILogger<ProjectService> logger,
            IStageRepository stageRepository,
            IJobRepository jobRepository)
        {
            _ciDataProviderFactory = ciDataProviderFactory;
            _cronJobsManager = cronJobsManager;
            _validationService = validationService;
            _projectRepository = projectRepository;
            _pipelineRepository = pipelineRepository;
            _panelRepository = panelRepository;
            _staticBranchPanelRepository = staticBranchPanelRepository;
            _configuration = configuration;
            _logger = logger;
            _stageRepository = stageRepository;
            _jobRepository = jobRepository;
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

        public async Task<ServiceObjectResult<Project>> UpdateProjectAsync(Project updatedProject)
        {
            var validationResult = await _validationService.ValidateAsync<UpdateProjectValidator, Project>(updatedProject);
            if (!validationResult.IsValid)
                return ServiceObjectResult<Project>.Error(validationResult);
        
            var project = await GetProjectByIdAsync(updatedProject.Id);

            //TODO: change when automapper
            project.ApiAuthenticationToken = updatedProject.ApiAuthenticationToken;
            project.ApiHostUrl = updatedProject.ApiHostUrl;
            project.ApiProjectId = updatedProject.ApiProjectId;
            project.DataProviderName = updatedProject.DataProviderName;
            project.CiDataUpdateCronExpression = updatedProject.CiDataUpdateCronExpression;

            _cronJobsManager.UpdateCiDataForProject(updatedProject);

            var r = await _projectRepository.UpdateAsync(project, updatedProject.Id);
            await _projectRepository.SaveAsync();

            return ServiceObjectResult<Project>.Ok(r);
        }

        public async Task<ServiceObjectResult<Project>> CreateProjectAsync(Project project)
        {
            var validationResult = await _validationService.ValidateAsync<CreateProjectValidator, Project>(project);
            if (!validationResult.IsValid)
                return ServiceObjectResult<Project>.Error(validationResult);
        
            var r = await _projectRepository.AddAsync(project);
            await _projectRepository.SaveAsync();

            _cronJobsManager.UpdateCiDataForProject(project);

            return ServiceObjectResult<Project>.Ok(r);
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

        private async Task DownloadNewestPipelinesNotInBrancNameList(ICiDataProvider dataProvider, Project project, int howMany, IEnumerable<string> branchNames, PipelinesMerger merger)
        {
            var branchNamesSet = new HashSet<string>(branchNames);
            var pageCounter = 0;
            var maxPagesToLookFor = int.Parse(_configuration["DataProviders:NewestPipelinesMaxPages"]);
            while (merger.NewestPipelinesCount < howMany && pageCounter++ <= maxPagesToLookFor)
            {
                var pagedNewest = await dataProvider.FetchNewestPipelines(project.ApiHostUrl, project.ApiAuthenticationToken, project.ApiProjectId, pageCounter, perPage: howMany);
                var pagePipelinesNotInLocalStatic = pagedNewest.pipelines.Where(p => !branchNamesSet.Contains(p.Ref));

                merger.AddPipelinesPageAtEnd(pagePipelinesNotInLocalStatic);

                if (pageCounter >= pagedNewest.totalPages) //If last page
                    break;
            }
        }

        /// <summary>
        ///     Downloads data from CiDataProvider for given project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>All pipelines</returns>
        public async Task UpdateCiDataForProjectAsync(int projectId)
        {
            var project = await GetProjectByIdAsync(projectId);
            if (project == null) return;

            PipelinesMerger merger = new PipelinesMerger();
            var dataProvider = _ciDataProviderFactory.CreateForProviderName(project.DataProviderName);

            var staticBranchesNamesDb = await _staticBranchPanelRepository.GetBranchNamesFromStaticPanelsForProject(projectId);
            var staticBranchesPipelines = await Task.WhenAll(staticBranchesNamesDb.Select(b => dataProvider.FetchPipeLineByBranch(project.ApiHostUrl, project.ApiAuthenticationToken, project.ApiProjectId, b)));

            var targetPipelineNumber = project.PipelinesNumber - project.Pipelines.Count;
            await DownloadNewestPipelinesNotInBrancNameList(dataProvider, project, targetPipelineNumber, staticBranchesNamesDb, merger);

            //Merge
            var mergeResult = merger.MergePipelines(project.Pipelines, staticBranchesPipelines, project.PipelinesNumber);
            _pipelineRepository.DeleteRange(project.Pipelines);

            //Save update to DB
            project.Pipelines = mergeResult.ToList();

            await _projectRepository.UpdateAsync(project, project.Id);
            await _projectRepository.SaveAsync();

            _logger.LogInformation($"Updated cidata for project: {project.Id}");
        }

        public async Task<IEnumerable<Pipeline>> GetPipelinesForPanel(int panelID)
        {
            var panel = (IPanelPipelines)(await _panelRepository.GetByIdAsync(panelID));
            var pipes = await panel.GetPipelinesDTOForPanel(_projectRepository);
            return pipes;
        }

        private async Task InsertPipelineToDB(Pipeline pipeline, Project project)
        {
            var staticBranchesNamesDb = await _staticBranchPanelRepository.GetBranchNamesFromStaticPanelsForProject(project.Id);
            if(staticBranchesNamesDb.Contains(pipeline.Ref))
            {
                //Insert at beginning
                project.Pipelines.ToList().Insert(0, pipeline);
            }
            else
            {
                //Insert after statics, on start of dynamics
                project.Pipelines.ToList().Insert(staticBranchesNamesDb.Count(), pipeline);
            }
            await _projectRepository.UpdateAsync(project, project.Id);
            await _projectRepository.SaveAsync();
        }

        #region Webhooks
        public void FireJobUpdate(string providerName, object body)
        {
            BackgroundJob.Enqueue<IProjectService>(s => s.WebhookJobUpdate(providerName, body));
        }

        public void FirePipelineUpdate(string providerName, object body)
        {
            BackgroundJob.Enqueue<IProjectService>(s => s.WebhookPipelineUpdate(providerName, body));
        }

        //public void FireProjectUpdate(string providerName, object body)
        //{
        //    BackgroundJob.Enqueue<IProjectService>(s => s.WebhookProjectUpdate(providerName, body));
        //}

        public async Task WebhookJobUpdate(string providerName, object body)
        {
            var dataProvider = _ciDataProviderFactory.CreateForProviderLowercaseName(providerName.ToLower());
            IProviderWithJobWebhook provider = dataProvider as IProviderWithJobWebhook;
            if (provider == null)
                return;

            await UpdatePipelineStage(provider.ExtractJobFromWebhook(body), provider, providerName);
        }

        public async Task WebhookPipelineUpdate(string providerName, object body)
        {
            var dataProvider = _ciDataProviderFactory.CreateForProviderLowercaseName(providerName.ToLower());
            IProviderWithPipelineWebhook provider = dataProvider as IProviderWithPipelineWebhook;
            if (provider == null)
                return;
            string apiProjectId = provider.ExtractProjectIdFromPipelineWebhook(body);

            var project = (await _projectRepository.FindOneByAsync(p => p.DataProviderName.Equals(providerName, StringComparison.OrdinalIgnoreCase) && p.ApiProjectId.Equals(apiProjectId)));
            await UpdatePipeline(provider.ExtractPipelineFromWebhook(body), project, dataProvider, providerName);
        }

        //public async Task WebhookProjectUpdate(string providerName, object body)
        //{
        //    var dataProvider = _ciDataProviderFactory.CreateForProviderLowercaseName(providerName.ToLower());
        //    string apiProjectId = dataProvider.GetProjectIdFromWebhookRequest(body);

        //    var project = (await _projectRepository.FindOneByAsync(p => p.DataProviderName.Equals(providerName, StringComparison.OrdinalIgnoreCase) && p.ApiProjectId.Equals(apiProjectId)));

        //    await UpdateCiDataForProjectAsync(project.Id);
        //}

        private async Task UpdatePipelineStage(Job job, IProviderWithJobWebhook provider, string providerName)
        {
            var repoJob = await _jobRepository.FindOneByAsync(j => j.DataProviderJobId == job.DataProviderJobId);
            if (repoJob == null)
                return;
            repoJob.Status = job.Status;
            await _jobRepository.UpdateAsync(repoJob, repoJob.Id);
            await _jobRepository.SaveAsync();

            var repoStage = repoJob.Stage;
            repoStage.StageStatus = provider.RecalculateStageStatus(repoStage.Jobs);
            await _stageRepository.UpdateAsync(repoStage, repoStage.Id);
            await _stageRepository.SaveAsync();
        }

        private async Task UpdatePipeline(Pipeline pipeline, Project project, ICiDataProvider dataProvider, string providerName)
        {
            var repoPipeline = await _pipelineRepository.FindOneByAsync(p => p.DataProviderPipelineId == pipeline.DataProviderPipelineId);
            if(repoPipeline == null)
            {
                //Add new to db
                repoPipeline = await dataProvider.FetchPipelineById(project.ApiHostUrl, project.ApiAuthenticationToken, project.ApiProjectId, pipeline.DataProviderPipelineId);
                await InsertPipelineToDB(repoPipeline, project);
            }
            else
            {
                repoPipeline = await dataProvider.FetchPipelineById(project.ApiHostUrl, project.ApiAuthenticationToken, project.ApiProjectId, repoPipeline.DataProviderPipelineId);
                await _pipelineRepository.UpdateAsync(repoPipeline, repoPipeline.Id);
                await _pipelineRepository.SaveAsync();
            }
        }

        #endregion
    }
}