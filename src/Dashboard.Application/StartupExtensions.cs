using System;
using Autofac;
using Dashboard.Application.AutofacModules;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Application.Services;
using Dashboard.Application.Validators;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces;
using Dashboard.Core.Interfaces.Repositories;
using Dashboard.Data.Repositories;
using FluentValidation;
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
            builder.RegisterAssemblyModules(typeof(ServiceModule).Assembly);

            builder.RegisterType<CronJobsManager>().As<ICronJobsManager>();

            builder.RegisterType<GitLabDataProvider>().As<ICiDataProvider>();
            builder.RegisterType<TravisDataProvider>().As<ICiDataProvider>();
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
