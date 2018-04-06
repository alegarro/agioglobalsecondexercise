using Microsoft.EntityFrameworkCore;
using PruebaUnoAgioGlobal.Core.Entities;
using PruebaUnoAgioGlobal.Core.Interfaces.Repositories;
using PruebaUnoAgioGlobal.Infrastructure.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaUnoAgioGlobal.Infrastructure.Repositories
{
    public class FlightsRepository : Repository<Flight>, IFlightsRepository
    {
        private readonly AppDbContext _dbContext;

        public FlightsRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Flight> GetFlightById(int id)
        {
            return await _dbContext.Flights
                            .Include(f => f.Airline)
                            .Include(f => f.OriginAirport)
                            .Include(f => f.DestinationAirport)
                            .Where(f => f.FlightId == id)
                            .FirstOrDefaultAsync();
            
        }

        public async Task<List<Flight>> GetAllFlights()
        {
            return await _dbContext.Flights
                            .Include(f => f.Airline)
                            .Include(f => f.OriginAirport)
                            .Include(f => f.DestinationAirport)
                            .ToListAsync();
        }       
    }
}