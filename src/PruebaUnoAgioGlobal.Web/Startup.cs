using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using PruebaUnoAgioGlobal.Infrastructure.Contexts;
using PruebaUnoAgioGlobal.Infrastructure.Migrations.Seedbeds;
using PruebaUnoAgioGlobal.Web.Extensions;
using PruebaUnoAgioGlobal.Web.Filters;
using System.Threading;

namespace PruebaUnoAgioGlobal.Web
{
    /// <summary>
    /// Class for the application startup.
    /// </summary>
    public class Startup
    {
        private static Mutex mutex = new Mutex();

        /// <summary>
        /// Generates a new instance of the class.
        /// </summary>
        /// <param name="config"></param>
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        /// <summary>
        /// Configuration of the application.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configuration of the application services.
        /// </summary>
        /// <param name="services">Services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            if (Configuration["environment"] != "Testing")
            {
                services.AddDbContext<AppDbContext>(options =>                
                    options.UseSqlite(Configuration.GetConnectionString("AgioGlobalDbLiteConnection")));
            }
            else
            {
                services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase("TestingDB"));
            }

            services.AddLocalization();

            services
                .AddMvc(options =>
                {
                    options.Filters.Add(typeof(ValidateRequestsActionFilter));
                })
                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Program>())
                .AddJsonOptions(options =>
                {
                    // Lower CamelCase
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            services.AddCustomSwagger(Configuration);

            // Avoid automapper registration twice, in order void an exception in tests launched in parallel
            mutex.WaitOne();
            Mapper.Reset();
            services.AddAutoMapper(typeof(Startup).Assembly);
            mutex.ReleaseMutex();

            services.AddInfraestructureServices();
            services.AddBusinessServices();
            services.AddApplicationServices();
        }        

        /// <summary>
        /// Application configuration.
        /// </summary>
        /// <param name="app">Application.</param>
        /// <param name="env">Environment.</param>
        /// <param name="loggerFactory">Logger.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddDebug();
            loggerFactory.AddFile("logs/PruebaUnoAgioGlobal.Web-{Date}.txt", LogLevel.Warning);

            // TODO: Change for production when restricted origins, methods and headers are specified
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseExceptionHandling();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/FlightsManagement/Error");
            }

            if (Configuration["environment"] != "Testing")
            {
                SeedDatabase.PopulateProductionData(app.ApplicationServices);

                // Enable Swagger
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            // Set other configurations settings
            app.UseStaticFiles();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=FlightsManagement}/{action=Index}/{id?}");
            });
        }
    }
}
