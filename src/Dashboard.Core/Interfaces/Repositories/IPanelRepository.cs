using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Core.Entities;

namespace Dashboard.Core.Interfaces.Repositories
{
    public interface IPanelRepository : IEfRepository<Panel>
    {
        Task<IEnumerable<string>> GetBranchNamesFromStaticPanelsForProject(int projectId);
    }
}
