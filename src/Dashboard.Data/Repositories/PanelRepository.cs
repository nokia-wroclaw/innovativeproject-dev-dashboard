using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces.Repositories;
using Dashboard.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Dashboard.Data.Repositories
{
    public class PanelRepository : EfRepository<Panel>, IPanelRepository
    {
        private IIncludableQueryable<Panel, object> EagerPanels => Context.Set<Panel>()
                                                                        .Include(p => p.Project)
                                                                            .ThenInclude(p => p.Pipelines)
                                                                        .Include(p => p.Position);

        public PanelRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Panel>> GetAllAsync()
        {
            return await EagerPanels
                .ToListAsync();
        }

        public override Task<Panel> GetByIdAsync(int id)
        {
            return EagerPanels
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<string>> GetBranchNamesFromStaticPanelsForProject(int projectId)
        {
            return await EagerPanels
                .Where(p => p.StaticBranchNames.Any() && p.Project.Id == projectId)
                .SelectMany(p => p.StaticBranchNames.Select(b => b.Name))
                .ToListAsync();
        }
    }
}
