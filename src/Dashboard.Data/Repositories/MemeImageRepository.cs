using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces.Repositories;
using Dashboard.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Data.Repositories
{
    public class MemeImageRepository : EfRepository<MemeImage>, IMemeImageRepository
    {
        public MemeImageRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MemeImage>> GetRandomMemes(int howMany)
        {
            return await Context.Set<MemeImage>()
                .GroupBy(x => x.ImageUrl)
                .Select(x => x.FirstOrDefault())
                .OrderBy(r => Guid.NewGuid())
                .Take(howMany)
                .ToListAsync();
        }
    }
}
