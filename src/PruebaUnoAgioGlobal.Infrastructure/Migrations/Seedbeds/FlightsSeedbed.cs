using PruebaUnoAgioGlobal.Core.Entities;
using PruebaUnoAgioGlobal.Infrastructure.Contexts;
using System.Linq;

namespace PruebaUnoAgioGlobal.Infrastructure.Migrations.Seedbeds
{
    public static class FlightsSeedbed
    {
        public static void PutSeeds(AppDbContext context)
        {
            if (!context.Flights.Any())
            {
                var flightsToInsert = new Flight[]
                {
                    new Flight()
                    {
                        FlightId = 1,
                        Code = "IB 4567",
                        AirlineId = 1,
                        OriginAirportId = 1,
                        DestinationAirportId = 2,
                        Distance = 434,
                        FuelConsumption = 3182,
                    },
                    new Flight()
                    {
                        FlightId = 2,
                        Code = "FR 7298",
                        AirlineId = 4,
                        OriginAirportId = 1,
                        DestinationAirportId = 4,
                        Distance = 2022,
                        FuelConsumption = 12710,
                    },                    
                };

                context.Flights.AddRange(flightsToInsert);
                
                context.SaveChanges();
            }
        }
    }
}
