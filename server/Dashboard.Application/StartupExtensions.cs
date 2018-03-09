using Dashboard.Application.Interfaces;
using Dashboard.Application.Services;
using Dashboard.Infrastructure.Data.Interfaces;
using Dashboard.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Dashboard.Application
{
    public static class StartupExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            //Register repositories
            services.AddTransient<IToDoItemRepository, ToDoItemRepository>();

            //Register services
            services.AddTransient<IToDoItemsService, ToDoItemsService>();
        }
    }
}
