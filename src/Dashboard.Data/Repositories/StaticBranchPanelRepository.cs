using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Data.Repositories
{
    public class StaticBranchPanelRepository : EfRepository<StaticBranchPanel>, IStaticBranchPanelRepository
    {
        public StaticBranchPanelRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<string>> GetBranchNamesFromStaticPanelsForProject(int projectId)
        {
            return await Context.Set<StaticBranchPanel>()
                .Select(p => p.StaticBranchName)
                .Distinct()
                .ToListAsync();
        }
    }
}
