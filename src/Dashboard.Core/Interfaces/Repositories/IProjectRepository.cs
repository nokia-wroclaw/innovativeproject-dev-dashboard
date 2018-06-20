using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces.CiProviders;

namespace Dashboard.Core.Interfaces.Repositories
{
    public interface IProjectRepository : IEfRepository<Project>
    {
        Task<Job> FindJobByDataProviderInfoAsync(DataProviderJobInfo jobInfo);
    }
}
