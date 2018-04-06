using PruebaUnoAgioGlobal.Web.ModelsApi.Airports.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaUnoAgioGlobal.Web.ApplicationServices.Interfaces
{
    /// <summary>
    /// Interface for the flights application service.
    /// </summary>
    public interface IAirportsService
    {
        /// <summary>
        /// Gets the airports list.
        /// </summary>
        /// <returns>Airports List.</returns>
        Task<List<AirportDTO>> GetAll();
    }
}
