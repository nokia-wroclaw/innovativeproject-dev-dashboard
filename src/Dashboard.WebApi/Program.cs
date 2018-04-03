using System;
using Dashboard.Data.Context;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Dashboard.WebApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetService<AppDbContext>();
                AppDbContextSeed.Seed(ctx);
            }

            host.Run();
        }

        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
		        .UseUrls($"http://*:{ Environment.GetEnvironmentVariable("PORT") ?? "5001" }/")
                .Build();
    }
}
