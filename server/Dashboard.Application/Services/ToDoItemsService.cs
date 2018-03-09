using System.Collections.Generic;
using System.Threading.Tasks;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces.Repositories;

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
