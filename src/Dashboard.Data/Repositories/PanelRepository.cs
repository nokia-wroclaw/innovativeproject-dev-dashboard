using System.Threading.Tasks;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces.Repositories;
using Dashboard.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Data.Repositories
{
    public class PanelRepository : EfRepository<Panel>, IPanelRepository
    {
        public PanelRepository(AppDbContext context) : base(context)
        {
        }

        public override Task<Panel> GetByIdAsync(int id)
        {
            return  Context.Set<Panel>()
                .Include(p => p.Project)
                .Include(p => p.Position)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
