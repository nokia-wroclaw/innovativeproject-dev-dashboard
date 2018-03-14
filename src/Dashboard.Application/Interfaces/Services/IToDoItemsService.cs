using System.Collections.Generic;
using System.Threading.Tasks;
using Dashboard.Core.Entities;

namespace Dashboard.Application.Interfaces.Services
{
    public interface IToDoItemsService
    {
        Task<IEnumerable<ToDoItem>> GetAllAsync();
    }
}
