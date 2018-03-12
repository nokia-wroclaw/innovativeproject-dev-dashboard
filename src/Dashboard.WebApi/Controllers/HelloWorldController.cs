using System.Threading.Tasks;
using Dashboard.Application.Interfaces.Services;
using Dashboard.WebApi.ApiModels;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class HelloWorldController : Controller
    {
        private readonly IToDoItemsService _toDoItemsService;

        public HelloWorldController(IToDoItemsService toDoItemsService)
        {
            _toDoItemsService = toDoItemsService;
        }

        // GET api/helloworld
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allItems = await _toDoItemsService.GetAllAsync();

            return Ok(allItems);
        }
    }
}