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
                                                                            .ThenInclude(p => p.StaticPipelines)
                                                                                .ThenInclude(p => p.Stages)
                                                                        .Include(p => p.Project)
                                                                            .ThenInclude(p => p.DynamicPipelines)
                                                                                .ThenInclude(p => p.Stages)
                                                                        .Include(p => p.Position);


        public PanelRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Panel>> GetAllAsync()
        {
            return await EagerPanels
                .ToListAsync();
        }

        public override async Task<Panel> GetByIdAsync(int id)
        {
            return await EagerPanels
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Project>> GetActiveProjects()
        {
            return await Context.Set<Panel>()
                .GroupBy(p => p.Project.Id)
                .Select(x => x.FirstOrDefault().Project)
                .ToListAsync();
        }
    }
}
