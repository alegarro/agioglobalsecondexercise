using Microsoft.EntityFrameworkCore;
using PruebaUnoAgioGlobal.Core.Entities;
using PruebaUnoAgioGlobal.Infrastructure.Contexts;
using System.Linq;

namespace PruebaUnoAgioGlobal.Infrastructure.Migrations.Seedbeds
{
    public static class AirportSeedbed
    {
        public static void PutSeeds(AppDbContext context)
        {
            if (!context.Airports.Any())
            {
                var airportsToInsert = new Airport[]
                {
                    new Airport()
                    {
                        AirportId = 1,
                        Code = "AGP",
                        Name = "Málaga - Costa del Sol",
                        Latitude = 36.674,
                        Longitude = -4.449,
                        Country = "Spain",
                    },
                    new Airport()
                    {
                        AirportId = 2,
                        Code = "MAD",
                        Name = "Madrid - Barajas Adolfo Suárez",
                        Latitude = 40.472,
                        Longitude = -3.561,
                        Country = "Spain",
                    },
                    new Airport()
                    {
                        AirportId = 3,
                        Code = "CDG",
                        Name = "París - Charles de Gaulle",
                        Latitude = 49.013,
                        Longitude = 2.550,
                        Country = "France",
                    },
                    new Airport()
                    {
                        AirportId = 4,
                        Code = "AMS",
                        Name = "Amsterdam - Schiphol",
                        Latitude = 52.309,
                        Longitude = 4.764,
                        Country = "Netherlands",
                    },
                    new Airport()
                    {
                        AirportId = 5,
                        Code = "CRL",
                        Name = "Brussels - Charleroi",
                        Latitude = 50.459,
                        Longitude = 4.454,
                        Country = "Belgium",
                    },
                };

                context.Airports.AddRange(airportsToInsert);

                context.SaveChanges();
            }
        }
    }
}
