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
        public PanelRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Panel>> GetAllAsync()
        {
            return await Context.Set<Panel>()
                .ToListAsync();
        }

        public override async Task<Panel> GetByIdAsync(int id)
        {
            return await Context.Set<Panel>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Project>> GetActiveProjects()
        {
            var xxx = await Context.Set<Blog>().FirstOrDefaultAsync();

            var proj = await Context.Set<Project>().ToListAsync();

            var panels = await Context.Set<Panel>().ToListAsync();

            var panelsWithProject = await Context.Set<Panel>().Where(p => p.Project != null).ToListAsync();

            var r = 

            await Context.Set<Panel>()
                .Where(p => p.Project != null)
                .GroupBy(p => p.Project.Id)
                .Select(x => x.FirstOrDefault().Project)
                .ToListAsync();

            return r;
        }
    }
}
