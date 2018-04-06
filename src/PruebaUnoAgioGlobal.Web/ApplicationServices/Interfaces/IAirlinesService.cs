using PruebaUnoAgioGlobal.Web.ModelsApi.Airlines.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaUnoAgioGlobal.Web.ApplicationServices.Interfaces
{
    /// <summary>
    /// Interface for the airlines application service.
    /// </summary>
    public interface IAirlinesService
    {
        /// <summary>
        /// Gets the airlines list.
        /// </summary>
        /// <returns>Airlines List.</returns>
        Task<List<AirlineDTO>> GetAll();
    }
}
