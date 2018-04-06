using Microsoft.Extensions.DependencyInjection;
using PruebaUnoAgioGlobal.Core.BusinessServices;
using PruebaUnoAgioGlobal.Core.Entities;
using PruebaUnoAgioGlobal.Core.Interfaces.BusinessServices;
using PruebaUnoAgioGlobal.Core.Interfaces.Repositories;
using PruebaUnoAgioGlobal.Infrastructure.Repositories;
using PruebaUnoAgioGlobal.Web.ApplicationServices;
using PruebaUnoAgioGlobal.Web.ApplicationServices.Interfaces;

namespace PruebaUnoAgioGlobal.Web.Extensions
{
    /// <summary>
    /// Static class for add services to the indepency injector.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Method that adds bussiness services.
        /// </summary>
        /// <param name="services">Services collection.</param>
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IFlightsBusinessService, FlightsBusinessService>();
        }

        /// <summary>
        /// Method that adds application services.
        /// </summary>
        /// <param name="services">Services collection.</param>
        public static void AddApplicationServices(this IServiceCollection services)
        {            
            services.AddScoped<IAirlinesService, AirlinesService>();
            services.AddScoped<IAirportsService, AirportsService>();
            services.AddScoped<IFlightsService, FlightsService>();
            services.AddScoped<IFilesService, FilesService>();
        }

        /// <summary>
        /// Method that adds infraestructure services.
        /// </summary>
        /// <param name="services">Services collection.</param>
        public static void AddInfraestructureServices(this IServiceCollection services)
        {
            // Generic
            services.AddScoped<IRepository<Airline>, Repository<Airline>>();
            services.AddScoped<IRepository<Airport>, Repository<Airport>>();
            services.AddScoped<IRepository<Flight>, Repository<Flight>>();

            // Specific
            services.AddScoped<IFlightsRepository, FlightsRepository>();
        }
    }
}
