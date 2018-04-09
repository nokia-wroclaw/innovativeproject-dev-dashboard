using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces.Repositories;
using Dashboard.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Dashboard.Data.Repositories
{
    public class ProjectRepository : EfRepository<Project>, IProjectRepository
    {
        private IIncludableQueryable<Project, object> EagerProjects => Context.Set<Project>()
                                                                        .Include(proj => proj.StaticPipelines)
                                                                            .ThenInclude(pipes => pipes.Stages)
                                                                        .Include(proj => proj.DynamicPipelines)
                                                                            .ThenInclude(pipes => pipes.Stages);

        public ProjectRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await EagerProjects
                .ToListAsync();
        }

        public override Task<Project> GetByIdAsync(int id)
        {
            return EagerProjects
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
