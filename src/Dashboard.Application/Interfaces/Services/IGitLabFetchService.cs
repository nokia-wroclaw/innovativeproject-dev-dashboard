using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Application.Interfaces.Services
{
    public interface IGitLabFetchService
    {
        IEnumerable<NGitLab.Models.Project> GetAllAccessibleProjects();
    }
}
