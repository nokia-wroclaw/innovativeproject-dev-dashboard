using System.Collections.Generic;
using System.Threading.Tasks;
using Dashboard.Application.Interfaces;
using Dashboard.Infrastructure.Data.Interfaces;
using Dashboard.Infrastructure.Models;

namespace Dashboard.Application.Services
{
    public class ToDoItemsService : IToDoItemsService
    {
        private readonly IToDoItemRepository _toDoItemRepository;

        public ToDoItemsService(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }

        public Task<IEnumerable<ToDoItem>> GetAllAsync()
        {
            return _toDoItemRepository.GetAllAsync();
        }
    }
}
