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
        Task<ProjectTile> GetTileById(int id);
        Task UpdatePipelinesForProjectAsync(int projectId);
    }
}
