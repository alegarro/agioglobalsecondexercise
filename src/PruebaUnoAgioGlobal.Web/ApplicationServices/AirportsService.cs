using AutoMapper;
using PruebaUnoAgioGlobal.Core.Entities;
using PruebaUnoAgioGlobal.Core.Interfaces.Repositories;
using PruebaUnoAgioGlobal.Web.ApplicationServices.Interfaces;
using PruebaUnoAgioGlobal.Web.ModelsApi.Airports.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaUnoAgioGlobal.Web.ApplicationServices
{
    /// <summary>
    /// Airports Application Service.
    /// </summary>
    public class AirportsService : IAirportsService
    {
        private readonly IRepository<Airport> _airportsRepository;

        /// <summary>
        /// Generates a new instance of the airport application service.
        /// </summary>
        /// <param name="airportsRepository">Airport Repository Instance.</param>
        public AirportsService(IRepository<Airport> airportsRepository)
        {
            _airportsRepository = airportsRepository;
        }

        /// <summary>
        /// Returns a list of the airports stored in the system.
        /// </summary>
        /// <returns>List of the airports.</returns>
        public async Task<List<AirportDTO>> GetAll()
        {
            var airportsEntityList = await _airportsRepository.List();

            return await Task.FromResult(Mapper.Map<List<AirportDTO>>(airportsEntityList));
        }
    }
}
