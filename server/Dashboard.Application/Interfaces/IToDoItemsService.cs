using System.Collections.Generic;
using System.Threading.Tasks;
using Dashboard.Infrastructure.Models;

namespace Dashboard.Application.Interfaces
{
    public interface IToDoItemsService
    {
        Task<IEnumerable<ToDoItem>> GetAllAsync();
    }
}
