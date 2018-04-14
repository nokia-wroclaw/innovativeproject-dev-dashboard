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
        private async Task<IEnumerable<Panel>> EagerPanels()
        {
                //Get all panels that have project (DynamicPipelinesPanel or StaticBranchPanel)
                var firstQuery = await Context.Set<Panel>()
                    .Include(p => p.Project)
                        .ThenInclude(p => p.StaticPipelines)
                            .ThenInclude(p => p.Stages)
                    .Include(p => p.Project)
                        .ThenInclude(p => p.DynamicPipelines)
                            .ThenInclude(p => p.Stages)
                    .Include(p => p.Position)
                    .ToListAsync();


                // Union with panels without project (MemePanel)
                var secondQuery = await Context.Set<Panel>()
                    .Include(p => p.Position)
                    .ToListAsync();

                return firstQuery.Union(secondQuery);
        }


        public PanelRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Panel>> GetAllAsync()
        {
            return await EagerPanels();
        }

        public override async Task<Panel> GetByIdAsync(int id)
        {
            return (await EagerPanels()).FirstOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<int>> GetActiveProjectIds()
        {
            return await Context.Set<Panel>()
                .Where(p => p.Project != null)
                .Select(p => p.Project.Id)
                .Distinct()
                .ToListAsync();
        }
    }
}
