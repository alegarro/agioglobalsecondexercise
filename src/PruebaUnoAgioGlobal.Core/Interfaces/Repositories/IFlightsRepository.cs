using PruebaUnoAgioGlobal.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaUnoAgioGlobal.Core.Interfaces.Repositories
{
    public interface IFlightsRepository : IRepository<Flight>
    {
        Task<Flight> GetFlightById(int id);
        Task<List<Flight>> GetAllFlights();
    }
}