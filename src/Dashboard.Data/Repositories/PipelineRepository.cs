
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces.Repositories;
using Dashboard.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Data.Repositories
{
    public class PipelineRepository : EfRepository<Pipeline>, IPipelineRepository
    {
        public PipelineRepository(AppDbContext context) : base(context)
        {
        }
    }
}
