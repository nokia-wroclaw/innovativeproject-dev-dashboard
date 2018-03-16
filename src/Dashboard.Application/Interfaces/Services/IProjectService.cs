using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces;

namespace Dashboard.Application.Interfaces.Services
{
    public interface IProjectService
    {
        Task<Project> GetProjectById(int projectId);
        Task UpdatePipelinesForProjectAsync(int projectId);
    }
}
