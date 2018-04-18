using System;
using Dashboard.Core.Interfaces;
using Dashboard.Data.Context;
using Hangfire;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Dashboard.WebApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetService<AppDbContext>();
                AppDbContextSeed.Seed(ctx);

                var cronManager = scope.ServiceProvider.GetService<ICronJobsManager>();
                cronManager.RegisterAllCronJobs();
            }

            host.Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
		        .UseUrls($"http://*:{ Environment.GetEnvironmentVariable("PORT") ?? "5001" }/");
    }
}
