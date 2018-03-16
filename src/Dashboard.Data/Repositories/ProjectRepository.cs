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
    public class ProjectRepository : EfRepository<Project>, IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context)
        {
        }

        public override Task<Project> GetByIdAsync(int id)
        {
            return Context.Set<Project>()
                .Include(p => p.Pipelines)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
