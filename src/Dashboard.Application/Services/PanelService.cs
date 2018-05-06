using System.Collections.Generic;
using System.Threading.Tasks;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Application.Validators;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces.Repositories;
using Dashboard.Data.Repositories;
using Hangfire;

namespace Dashboard.Application.Services
{
    //TODO: add some validation
    public class PanelService : IPanelService
    {
        private readonly IPanelRepository _panelRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IValidationService _validationService;

        public PanelService(IPanelRepository panelRepository, IProjectRepository projectRepository, IValidationService validationService)
        {
            _panelRepository = panelRepository;
            _projectRepository = projectRepository;
            _validationService = validationService;
        }

        public Task<Panel> GetPanelByIdAsync(int id)
        {
            return _panelRepository.GetByIdAsync(id);
        }

        public Task<IEnumerable<Panel>> GetAllPanelsAsync()
        {
            return _panelRepository.GetAllAsync();
        }

        public async Task DeletePanelAsync(int id)
        {
            var enitty = await GetPanelByIdAsync(id);
            if (enitty == null)
                return;

            _panelRepository.Delete(enitty);
            await _panelRepository.SaveAsync();
        }

        public async Task<ServiceObjectResult<Panel>> UpdatePanelAsync(Panel updatedPanel)
        {
            var validationResult = await _validationService.ValidateAsync<UpdatePanelValidator, Panel>(updatedPanel);
            if (!validationResult.IsValid)
                return ServiceObjectResult<Panel>.Error(validationResult);

            var model = await GetPanelByIdAsync(updatedPanel.Id);
            if (model == null) return null;

            if (updatedPanel.Discriminator != model.Discriminator) return null;

            if (updatedPanel.ProjectId.HasValue)
            {
                var project = await _projectRepository.GetByIdAsync(updatedPanel.ProjectId.Value);
                model.Project = project;
            }

            var r = await _panelRepository.UpdateAsync(updatedPanel, model.Id);
            await _panelRepository.SaveAsync();

            return ServiceObjectResult<Panel>.Ok(r);
        }

        public async Task<ServiceObjectResult<Panel>> CreatePanelAsync(Panel model)
        {
            var validationResult = await _validationService.ValidateAsync<CreatePanelValidator, Panel>(model);
            if (!validationResult.IsValid)
                return ServiceObjectResult<Panel>.Error(validationResult);

            if (model.ProjectId.HasValue)
            {
                var existingProject = await _projectRepository.GetByIdAsync(model.ProjectId.Value);
                model.Project = existingProject;
            }

            var r = await _panelRepository.AddAsync(model);
            await _panelRepository.SaveAsync();

            if(model.Project != null)
                BackgroundJob.Enqueue<IProjectService>(s => s.UpdateCiDataForProjectAsync(model.Project.Id));

            return ServiceObjectResult<Panel>.Ok(r);
        }

        public async Task<IEnumerable<Project>> GetActiveProjects()
        {
            return await _panelRepository.GetActiveProjects();
        }

        public async Task<Panel> UpdatePanelPosition(int panelId, PanelPosition position)
        {
            var entity = await GetPanelByIdAsync(panelId);
            if (entity == null) return null;

            //TODO: change when automaper
            entity.Position.Column = position.Column;
            entity.Position.Row = position.Row;
            entity.Position.Width = position.Width;
            entity.Position.Height = position.Height;

            var r = await _panelRepository.UpdateAsync(entity, panelId);
            await _panelRepository.SaveAsync();

            return r;
        }
    }
}
