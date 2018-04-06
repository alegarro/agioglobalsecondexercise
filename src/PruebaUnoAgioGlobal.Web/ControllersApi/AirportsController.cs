using Ardalis.Filters;
using Microsoft.AspNetCore.Mvc;
using PruebaUnoAgioGlobal.Web.ApplicationServices.Interfaces;
using PruebaUnoAgioGlobal.Web.Exceptions.Entities;
using PruebaUnoAgioGlobal.Web.ModelsApi.Airports.Responses;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PruebaUnoAgioGlobal.Web.ControllersApi
{
    /// <summary>
    /// Airports Controller.
    /// </summary>
    [Route("api/airports")]
    [ValidateModel]
    public class AirportsController : Controller
    {
        private readonly IAirportsService _airportsService;

        /// <summary>
        /// Generates a new instance of the airports controller.
        /// </summary>
        /// <param name="airportsService">Airports Service.</param>
        public AirportsController(IAirportsService airportsService)
        {
            _airportsService = airportsService;
        }

        // GET: api/airports
        /// <summary>
        /// Method that gets the airports list.
        /// </summary>
        /// <returns>Airports list.</returns>              
        [HttpGet]
        [SwaggerOperation("Gets the airports stored in the system.")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(List<AirportDTO>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, typeof(InternalServerExceptionInfo))]
        public async Task<IActionResult> GetAll()
        {
            var airportsListDtos = await _airportsService.GetAll();

            return new OkObjectResult(airportsListDtos);
        }
    }
}
