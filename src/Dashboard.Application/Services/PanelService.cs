using System.Collections.Generic;
using System.Threading.Tasks;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces.Repositories;

namespace Dashboard.Application.Services
{
    //TODO: add some validation
    public class PanelService : IPanelService
    {
        private readonly IPanelRepository _panelRepository;
        private readonly IProjectRepository _projectRepository;

        public PanelService(IPanelRepository panelRepository, IProjectRepository projectRepository)
        {
            _panelRepository = panelRepository;
            _projectRepository = projectRepository;
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

            await _panelRepository.DeleteAsync(enitty);
            await _panelRepository.SaveAsync();
        }

        public async Task<Panel> UpdatePanelAsync(Panel updatedPanel, int projectId)
        {
            var model = await GetPanelByIdAsync(updatedPanel.Id);
            if (model == null)
                return null;

            var project = await _projectRepository.GetByIdAsync(projectId);
            model.Project = project;

            //TODO: change when automapper
            model.Data = updatedPanel.Data;
            model.Dynamic = updatedPanel.Dynamic;
            model.Title = updatedPanel.Title;
            model.Type = updatedPanel.Type;
            model.Position.Column = updatedPanel.Position.Column;
            model.Position.Row = updatedPanel.Position.Row;

            var r = await _panelRepository.UpdateAsync(model, updatedPanel.Id);
            await _panelRepository.SaveAsync();

            return r;
        }

        public async Task<Panel> CreatePanelAsync(Panel model, int projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            model.Project = project;

            var r = await _panelRepository.AddAsync(model);
            await _panelRepository.SaveAsync();

            return r;
        }
    }
}
