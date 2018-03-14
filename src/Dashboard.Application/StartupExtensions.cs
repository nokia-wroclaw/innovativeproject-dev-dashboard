using Dashboard.Application.Interfaces.Services;
using Dashboard.Application.Services;
using Dashboard.Core.Interfaces.Repositories;
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
