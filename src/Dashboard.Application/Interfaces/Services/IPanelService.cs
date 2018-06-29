using System.Collections.Generic;
using System.Threading.Tasks;
using Dashboard.Core.Entities;

namespace Dashboard.Application.Interfaces.Services
{
    public interface IPanelService
    {
        Task<Panel> GetPanelByIdAsync(int id);
        Task<IEnumerable<Panel>> GetAllPanelsAsync();
        Task DeletePanelAsync(int id);

        Task<ServiceObjectResult<Panel>> UpdatePanelAsync(Panel updatedPanel);
        Task<ServiceObjectResult<Panel>> CreatePanelAsync(Panel model);

        /// <summary>
        /// Returns list of projects that are referenced by any panel
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Project>> GetActiveProjects();

        Task<ServiceObjectResult<Panel>> UpdatePanelPosition(int panelId, PanelPosition position);
    }
}
