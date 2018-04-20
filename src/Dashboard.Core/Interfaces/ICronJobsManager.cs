using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Core.Entities;

namespace Dashboard.Core.Interfaces
{
    public interface ICronJobsManager
    {
        void RegisterAllCronJobs();

        void UpdateCiDataForProject(Project project);
        void UnregisterUpdateCiDataForProject(int projectId);
        void FireProjectUpdate(int projectId);
    }
}
