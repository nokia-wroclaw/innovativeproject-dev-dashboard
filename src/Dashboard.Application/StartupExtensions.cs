﻿using System;
using Autofac;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Application.Services;
using Dashboard.Core.Interfaces;
using Dashboard.Core.Interfaces.Repositories;
using Dashboard.Data.Repositories;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Dashboard.Application
{
    public static class StartupExtensions
    {
        public static void AddApplication(this ContainerBuilder builder)
        {
            //Register repositories
            builder.RegisterType<PanelRepository>().As<IPanelRepository>();
            //builder.RegisterType<StaticBranchPanelRepository>().As<IStaticBranchPanelRepository>();
            builder.RegisterType<PipelineRepository>().As<IPipelineRepository>();
            builder.RegisterType<ProjectRepository>().As<IProjectRepository>();
            //builder.RegisterType<DynamicPipelinePanelRepository>().As<IDynamicPipelinePanelRepository>();

            //Register services
            builder.RegisterType<PanelService>().As<IPanelService>();
            builder.RegisterType<ProjectService>().As<IProjectService>();

            builder.RegisterType<CronJobsManager>().As<ICronJobsManager>();
            builder.RegisterType<GitLabDataProvider>().As<ICiDataProvider>();
            builder.RegisterType<CiDataProviderFactory>().As<ICiDataProviderFactory>();
        }

        public static void AddAppHangfire(this IServiceCollection services)
        {
            //Hangfire
            var inMemory = GlobalConfiguration.Configuration.UseMemoryStorage(new MemoryStorageOptions()
            {
                JobExpirationCheckInterval = TimeSpan.FromMinutes(15)
            });
            services.AddHangfire(c =>
            {
                c.UseStorage(inMemory);
            });
        }
    }
}
