using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces;

namespace Dashboard.Application.Interfaces.Services
{
    public interface IProjectTileService
    {
        Task<ProjectTile> GetTileByIdAsync(int id);
        Task<IEnumerable<ProjectTile>> GetAllTilesAsync();
        Task DeleteTileAsync(int id);
        Task<ProjectTile> UpdateTileAsync(ProjectTile updatedTile);
        Task<ProjectTile> CreateTileAsync(ProjectTile tile);

        Task UpdatePipelinesForProjectAsync(int projectId);
    }
}
