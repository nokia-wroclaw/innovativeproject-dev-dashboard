using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces.CiProviders;
using Dashboard.Core.Interfaces.Repositories;
using Dashboard.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Dashboard.Data.Repositories
{
    public class ProjectRepository : EfRepository<Project>, IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await Context.Set<Project>()
                .ToListAsync();
        }

        public override Task<Project> GetByIdAsync(int id)
        {
            return Context.Set<Project>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Job> FindJobByDataProviderInfoAsync(DataProviderJobInfo jobInfo)
        {
            return Context.Set<Project>()
                .Where(p => p.ApiProjectId == jobInfo.ProjectId && p.DataProviderName == jobInfo.ProviderName)
                .SelectMany(p => p.Pipelines)
                .SelectMany(pip => pip.Stages)
                .SelectMany(s => s.Jobs)
                .FirstOrDefaultAsync(j => j.DataProviderJobId == jobInfo.JobId);
        }

        public async Task<(Project, Pipeline)> FindProjectByDataProviderInfoAsync(DataProviderPipelineInfo pipelineInfo)
        {
            int pipeIdINT = 0;
            if (!int.TryParse(pipelineInfo.PipelineId, out pipeIdINT))
                return (null, null);
            var project = await Context.Set<Project>().FirstOrDefaultAsync(p => p.ApiProjectId == pipelineInfo.ProjectId && p.DataProviderName == pipelineInfo.ProviderName);
            var pipe = project.Pipelines.FirstOrDefault(j => j.DataProviderPipelineId == pipeIdINT);

            return (project, pipe); 
        }
    }
}
