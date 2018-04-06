using AutoMapper;
using PruebaUnoAgioGlobal.Core.Entities;
using PruebaUnoAgioGlobal.Core.Interfaces.Repositories;
using PruebaUnoAgioGlobal.Web.ApplicationServices.Interfaces;
using PruebaUnoAgioGlobal.Web.ModelsApi.Airlines.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaUnoAgioGlobal.Web.ApplicationServices
{
    /// <summary>
    /// Airlines Application Service.
    /// </summary>
    public class AirlinesService : IAirlinesService
    {
        private readonly IRepository<Airline> _airlinesRepository;

        /// <summary>
        /// Generates a new instance of the airline application service.
        /// </summary>
        /// <param name="airlinesRepository">Airlines Repository Instance.</param>
        public AirlinesService(IRepository<Airline> airlinesRepository)
        {
            _airlinesRepository = airlinesRepository;
        }

        /// <summary>
        /// Returns a list of the airlines stored in the system.
        /// </summary>
        /// <returns>List of the airlines.</returns>
        public async Task<List<AirlineDTO>> GetAll()
        {
            var airlinesEntityList = await _airlinesRepository.List();

            return await Task.FromResult(Mapper.Map<List<AirlineDTO>>(airlinesEntityList));
        }
    }
}
