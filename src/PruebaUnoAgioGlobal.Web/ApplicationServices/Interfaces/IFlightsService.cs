using PruebaUnoAgioGlobal.Web.ModelsApi.Flights.Requests;
using PruebaUnoAgioGlobal.Web.ModelsApi.Flights.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaUnoAgioGlobal.Web.ApplicationServices.Interfaces
{
    /// <summary>
    /// Interface for the flights application service.
    /// </summary>
    public interface IFlightsService
    {
        /// <summary>
        /// Gets the flights list.
        /// </summary>
        /// <returns>Flights List.</returns>
        Task<List<FlightListDTO>> GetAll();

        /// <summary>
        /// Gets a flight by Id.
        /// </summary>
        /// <param name="request">Request with the id of the flight to get.</param>
        /// <returns>Flight data.</returns>
        Task<FlightDTO> GetById(GetFlightByIdRequest request);

        /// <summary>
        /// Inserts a new flight in the database.
        /// </summary>
        /// <param name="request">Flight data to insert in the database.</param>
        Task<int> Insert(CreateFlightRequest request);

        /// <summary>
        /// Updates a flight in the database.
        /// </summary>
        /// <param name="request">Flight data to update in the database.</param>
        Task Update(UpdateFlightRequest request);

        /// <summary>
        /// Deletes a flight in the database.
        /// </summary>
        /// <param name="request">Request with the id of the flight to delete.</param>
        Task Delete(DeleteFlightRequest request);
    }
}
