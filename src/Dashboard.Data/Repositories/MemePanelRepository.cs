using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces.Repositories;
using Dashboard.Data.Context;

namespace Dashboard.Data.Repositories
{
    public class MemePanelRepository : EfRepository<MemePanel>, IMemePanelRepository
    {
        public MemePanelRepository(AppDbContext context) : base(context)
        {
        }
    }
}
