using Dashboard.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Application.Services
{
    public class GitLabFetchService : IGitLabFetchService
    {
        public GitLabFetchService()
        {
        }

        public IEnumerable<NGitLab.Models.Project> GetAllAccessibleProjects()
        {
            return NGitLab.GitLabClient.Connect("https://gitlab.com", "wL6jWfdAuqhqZ_MzERk1").Projects.Accessible;
        }
    }
}
