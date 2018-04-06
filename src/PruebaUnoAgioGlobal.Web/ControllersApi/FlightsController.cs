using Ardalis.Filters;
using Microsoft.AspNetCore.Mvc;
using PruebaUnoAgioGlobal.Web.ApplicationServices.Interfaces;
using PruebaUnoAgioGlobal.Web.Exceptions.Entities;
using PruebaUnoAgioGlobal.Web.ModelsApi.Flights.Requests;
using PruebaUnoAgioGlobal.Web.ModelsApi.Flights.Responses;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PruebaUnoAgioGlobal.Web.ControllersApi
{
    /// <summary>
    /// Flights Controller.
    /// </summary>
    [Route("api/flights")]
    [ValidateModel]
    public class FlightsController : Controller
    {
        private readonly IFlightsService _flightsService;

        /// <summary>
        /// Generates a new instance of the flights controller.
        /// </summary>
        /// <param name="flightsService">Flights Service.</param>
        public FlightsController(IFlightsService flightsService)
        {
            _flightsService = flightsService;
        }

        // GET: api/flights
        /// <summary>
        /// Method that gets the flights list.
        /// </summary>
        /// <returns>Flights list.</returns>              
        [HttpGet]
        [SwaggerOperation("Gets the flights stored in the system.")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(List<FlightDTO>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, typeof(InternalServerExceptionInfo))]
        public async Task<IActionResult> GetAll()
        {
            var flightsListDtos = await _flightsService.GetAll();

            return new OkObjectResult(flightsListDtos);
        }

        // GET: api/flights/id
        /// <summary>
        /// Method that gets a flights with the requested id.
        /// </summary>
        /// <returns>Flight with the requested id.</returns>              
        [HttpGet("{FlightId}", Name = nameof(GetById))]
        [SwaggerOperation("Gets the flight with the requested id stored in the system.")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(FlightDTO))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, typeof(NotFoundExceptionInfo))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, typeof(InternalServerExceptionInfo))]
        public async Task<IActionResult> GetById([FromRoute]GetFlightByIdRequest request)
        {
            var flightDto = await _flightsService.GetById(request);

            return new OkObjectResult(flightDto);
        }

        // POST: api/flights
        /// <summary>
        /// Method that creates a new flight in the database.
        /// </summary>             
        [HttpPost]
        [SwaggerOperation("Creates a new flight in the database.")]
        [SwaggerResponse((int)HttpStatusCode.Created)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, typeof(InternalServerExceptionInfo))]
        public async Task<IActionResult> CreateFlight([FromBody]CreateFlightRequest request)
        {
            var createdFlightId = await _flightsService.Insert(request);

            var createdFlightGetByIdRequest = new GetFlightByIdRequest() { FlightId = createdFlightId };

            return new CreatedAtRouteResult(nameof(this.GetById), createdFlightGetByIdRequest, null);
        }

        // PUT: api/flights/id
        /// <summary>
        /// Method that updates a flight with the requested id in the database.
        /// </summary>            
        [HttpPut("{FlightId}")]
        [SwaggerOperation("Updates a flight with the requested id in the database.")]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.NotFound, typeof(NotFoundExceptionInfo))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, typeof(InternalServerExceptionInfo))]
        public async Task<IActionResult> UpdateFlight([FromRoute]int FlightId, [FromBody]UpdateFlightRequest request)
        {
            request.FlightId = FlightId;

            await _flightsService.Update(request);

            return new NoContentResult();
        }

        // DELETE: api/flights/id
        /// <summary>
        /// Method that removes a flight with the requested id from the database.
        /// </summary>           
        [HttpDelete("{FlightId}")]
        [SwaggerOperation("Removes a flight with the requested id from the database.")]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.NotFound, typeof(NotFoundExceptionInfo))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, typeof(InternalServerExceptionInfo))]
        public async Task<IActionResult> DeleteFlight([FromRoute]DeleteFlightRequest request)
        {
            await _flightsService.Delete(request);

            return new NoContentResult();
        }
    }
}
