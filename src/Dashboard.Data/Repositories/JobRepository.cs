using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces.Repositories;
using Dashboard.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Data.Repositories
{
    public class JobRepository : EfRepository<Job>, IJobRepository
    {
        public JobRepository(AppDbContext context) : base(context)
        {

        }
    }
}
