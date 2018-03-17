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

        public PanelService(IPanelRepository panelRepository)
        {
            _panelRepository = panelRepository;
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

        public async Task<Panel> UpdatePanelAsync(Panel updatedPanel)
        {
            var model = await GetPanelByIdAsync(updatedPanel.Id);
            if (model == null)
                return null;

            //TODO: change when automapper
            model.Data = updatedPanel.Data;
            model.Position = updatedPanel.Position;
            model.Dynamic = updatedPanel.Dynamic;
            model.Title = updatedPanel.Title;
            model.Type = updatedPanel.Type;

            var r = await _panelRepository.UpdateAsync(model, updatedPanel.Id);
            await _panelRepository.SaveAsync();

            return r;
        }

        public async Task<Panel> CreatePanelAsync(Panel model)
        {
            var r = await _panelRepository.AddAsync(model);
            await _panelRepository.SaveAsync();

            return r;
        }
    }
}
