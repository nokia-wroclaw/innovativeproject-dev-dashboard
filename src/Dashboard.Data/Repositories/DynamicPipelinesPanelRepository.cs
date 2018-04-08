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
    public class DynamicPipelinesPanelRepository : EfRepository<DynamicPipelinesPanel>, IDynamicPipelinesPanelRepository
    {
        private IIncludableQueryable<DynamicPipelinesPanel, object> EagerPanels => Context.Set<DynamicPipelinesPanel>()
                                                                        .Include(p => p.Project)
                                                                            .ThenInclude(p => p.Pipelines)
                                                                                .ThenInclude(p => p.Stages)
                                                                        .Include(p => p.Position);

        public DynamicPipelinesPanelRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<DynamicPipelinesPanel>> GetDynamicPanelsForProject(int projectId)
        {
            return (await EagerPanels.ToListAsync()).Where(p => p.ProjectId == projectId);
        }

        public async Task<int> GetNumberOfDiscoverPipelinesForProject(int projectId)
        {
            return (await EagerPanels.ToListAsync()).Where(p => p.ProjectId == projectId).Sum(p => p.HowManyLastPipelinesToRead);
        }
    }
}
