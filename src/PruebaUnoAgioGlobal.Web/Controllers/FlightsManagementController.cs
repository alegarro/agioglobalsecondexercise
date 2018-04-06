using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PruebaUnoAgioGlobal.Web.ApplicationServices.Interfaces;
using PruebaUnoAgioGlobal.Web.ModelsApi.Flights.Requests;
using System.Threading.Tasks;

namespace PruebaUnoAgioGlobal.Web.Controllers
{
    /// <summary>
    /// Controller for flights management views.
    /// </summary>
    public class FlightsManagementController : Controller
    {
        private readonly IFlightsService _flightsService;

        /// <summary>
        /// Generates a new instance of the Flights Management Controller.
        /// </summary>
        /// <param name="flightsService">Flights application service.</param>
        public FlightsManagementController(IFlightsService flightsService)
        {
            _flightsService = flightsService;
        }

        /// <summary>
        /// Loads the main flight management view.
        /// </summary>
        /// <returns>Flight management view.</returns>
        public async Task<IActionResult> Index()
        {
            var flightsDtos = await _flightsService.GetAll();

            return View(flightsDtos);
        }

        /// <summary>
        /// Loads the create flight view.
        /// </summary>
        public IActionResult Create()
        {
            var createFlightRequest = new CreateFlightRequest();

            return View(createFlightRequest);
        }

        /// <summary>
        /// Stores a new flight.
        /// </summary>
        /// <param name="request">Request to create a new flight.</param>
        /// <returns>Redirect to index view.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateFlightRequest request)
        {
            var insertedId = await _flightsService.Insert(request);

            return new RedirectResult(Url.Action(nameof(Index)));
        }

        /// <summary>
        /// Loads the edit flight view.
        /// </summary>
        /// <returns>Flight edit view.</returns>
        public async Task<IActionResult> Edit(int flightId)
        {
            var getFlightByIdRequest = new GetFlightByIdRequest() { FlightId = flightId };

            var flightDto = await _flightsService.GetById(getFlightByIdRequest);

            return View(Mapper.Map<UpdateFlightRequest>(flightDto));
        }

        /// <summary>
        /// Updates a flight.
        /// </summary>
        /// <param name="request">Request to update a flight.</param>
        /// <returns>Redirect to index view.</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateFlightRequest request)
        {
            await _flightsService.Update(request);

            return new RedirectResult(Url.Action(nameof(Index)));
        }

        /// <summary>
        /// Deletes selected flight.
        /// </summary>
        /// <returns>Flight id for delete.</returns>
        /// <returns>Redirect to index view.</returns>
        public async Task<IActionResult> Delete(int flightId)
        {
            var deleteFlightRequest = new DeleteFlightRequest() { FlightId = flightId };

            await _flightsService.Delete(deleteFlightRequest);

            return new RedirectResult(Url.Action(nameof(Index)));
        }

        /// <summary>
        /// Loads the error view.
        /// </summary>
        /// <returns>Error view.</returns>
        public IActionResult Error()
        {
            return View();
        }
    }
}
