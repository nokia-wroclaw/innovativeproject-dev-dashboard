using Dashboard.Application.Interfaces.Services;
using Dashboard.Application.Services;
using Dashboard.Core.Interfaces;
using Dashboard.Core.Interfaces.Repositories;
using Dashboard.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Dashboard.Application
{
    public static class StartupExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            //Register repositories
            services.AddTransient<IPanelRepository, PanelRepository>();
            services.AddTransient<IPipelineRepository, PipelineRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();

            //Register services
            services.AddTransient<IPanelService, PanelService>();
            services.AddTransient<IProjectService, ProjectService>();

            services.AddScoped<ICIDataProvider, GitLabDataProvider>();
            services.AddTransient<ICIDataProviderFactory, CIDataProviderFactory>();
        }
    }
}
