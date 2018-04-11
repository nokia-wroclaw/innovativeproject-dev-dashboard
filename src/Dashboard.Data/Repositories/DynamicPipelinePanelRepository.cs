using System;
using System.Collections.Generic;
using System.Text;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Data.Context;

namespace Dashboard.Data.Repositories
{
    public class DynamicPipelinePanelRepository : EfRepository<DynamicPipelinesPanel>, IDynamicPipelinePanelRepository
    {
        private IIncludableQueryable<DynamicPipelinesPanel, object> EagerPanels => Context.Set<DynamicPipelinesPanel>()
                                                                        .Include(p => p.Project)
                                                                            .ThenInclude(p => p.DynamicPipelines)
                                                                                .ThenInclude(p => p.Stages)
                                                                        .Include(p => p.Position);

        public DynamicPipelinePanelRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<DynamicPipelinesPanel>> GetDynamicPanelsForProject(int projectId)
        {
            return (await EagerPanels.Where(p => p.ProjectId == projectId).ToListAsync());
        }

        public async Task<int> GetNumberOfDiscoverPipelinesForProject(int projectId)
        {
            return await Context.Set<DynamicPipelinesPanel>().Where(p => p.ProjectId == projectId).SumAsync(p => p.HowManyLastPipelinesToRead);
        }
    }
}
