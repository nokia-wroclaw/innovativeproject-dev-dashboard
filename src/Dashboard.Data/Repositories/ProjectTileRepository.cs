using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces.Repositories;
using Dashboard.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Data.Repositories
{
    public class ProjectTileRepository : EfRepository<ProjectTile>, IProjectTileRepository
    {
        public ProjectTileRepository(AppDbContext context) : base(context)
        {
        }

        public override Task<ProjectTile> GetByIdAsync(int id)
        {
            return Context.Set<ProjectTile>()
                .Include(p => p.Pipelines)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
