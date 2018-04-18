using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Dashboard.Application;
using Dashboard.Core.Interfaces;
using Dashboard.Data.Context;
using Dashboard.WebApi.Infrastructure;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Examples;
using Swashbuckle.AspNetCore.Swagger;

namespace Dashboard.WebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        private IConfigurationRoot Configuration { get; set; }
        private IContainer ApplicationContainer { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddAppHangfire();

            //TODO: change when database is setup
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemoryDatabase"));

            services.AddMvc(options =>
            {
            });
            //services.AddMvcCore().AddJsonFormatters(f => f.Converters.Add(new StringEnumConverter()));

            //OpenAPI for sweet swagger documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.OperationFilter<ExamplesOperationFilter>(); // [SwaggerRequestExample] & [SwaggerResponseExample]
                c.OperationFilter<DescriptionOperationFilter>(); // [Description] on Response properties
                //c.OperationFilter<AuthorizationInputOperationFilter>(); // Adds an Authorization input box to every endpoint
                //c.OperationFilter<AddFileParamTypesOperationFilter>(); // Adds an Upload button to endpoints which have [AddSwaggerFileUploadButton]
                //c.OperationFilter<AddHeaderOperationFilter>("correlationId", "Correlation Id for the request"); // adds any string you like to the request headers - in this case, a correlation id
                c.OperationFilter<AddResponseHeadersFilter>(); // [SwaggerResponseHeader]
                //c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>(); // Adds "(Auth)" to the summary so that you can see which endpoints have Authorization
            });

            // Create the container builder.
            var builder = new ContainerBuilder();
            builder.Populate(services);

            //Register app
            builder.AddApplication();

            this.ApplicationContainer = builder.Build();

            GlobalConfiguration.Configuration.UseAutofacActivator(ApplicationContainer);

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseApplicationHttpRequestExceptionMiddleware();
            }
            else
            {
                app.UseApplicationHttpRequestExceptionMiddleware();
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app
                .UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });

            app.UseHangfireServer().UseHangfireDashboard(options: new DashboardOptions()
            {
                AppPath = "/hangfire",
                Authorization = new[] { new HangfireAuthorizationFilter(), }
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "swagger");
            });
        }
    }
}
