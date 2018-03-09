using Dashboard.Infrastructure.Data.Context;
using Dashboard.Infrastructure.Data.Interfaces;
using Dashboard.Infrastructure.Models;

namespace Dashboard.Infrastructure.Data.Repositories
{
    public class ToDoItemRepository : EfRepository<ToDoItem>, IToDoItemRepository
    {
        public ToDoItemRepository(AppDbContext context) : base(context)
        {
        }
    }
}
