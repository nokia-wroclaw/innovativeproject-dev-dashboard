using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Application.Interfaces;
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
        public async Task<JsonResult> Get()
        {
            var allItems = await _toDoItemsService.GetAllAsync();

            return Json(new
            {
                Documentation = "/swagger/v1/swagger.json",

                SwaggerEditor = "https://editor.swagger.io/",

                ToDoItems = allItems
            });
        }
    }
}