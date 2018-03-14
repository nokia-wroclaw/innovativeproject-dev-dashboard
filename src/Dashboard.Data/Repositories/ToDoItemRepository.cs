using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces.Repositories;
using Dashboard.Data.Context;
using Dashboard.Data.Repositories;

namespace Dashboard.Infrastructure.Data.Repositories
{
    public class ToDoItemRepository : EfRepository<ToDoItem>, IToDoItemRepository
    {
        public ToDoItemRepository(AppDbContext context) : base(context)
        {
        }
    }
}
