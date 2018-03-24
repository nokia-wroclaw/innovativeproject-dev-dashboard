using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces.Repositories;
using Dashboard.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Data.Repositories
{
    public class PanelRepository : EfRepository<Panel>, IPanelRepository
    {
        private async Task<IEnumerable<Panel>> EagerPanels()
        {
            var panels = await Context.Set<Panel>()
                .Include(p => p.Project)
                    .ThenInclude(p => p.Pipelines)
                .Include(p => p.Position)
                .ToListAsync();

            panels.ForEach(async panel =>
            {
                if (panel is StaticBranchPanel x)
                {
                    x.StaticBranchNames = await Context.Set<StaticBranchPanel>()
                        .Where(p => p.Id == x.Id)
                        .SelectMany(a => a.StaticBranchNames)
                        .ToListAsync();
                }
            });

            return panels;
        }

        public PanelRepository(AppDbContext context) : base(context)
        {
        }

        public override async  Task<IEnumerable<Panel>> GetAllAsync()
        {
            return await EagerPanels();
        }

        public override async Task<Panel> GetByIdAsync(int id)
        {
            return (await EagerPanels()).FirstOrDefault(x => x.Id == id);
        }
    }
}
