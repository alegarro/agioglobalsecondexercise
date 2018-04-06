using Microsoft.EntityFrameworkCore;
using PruebaUnoAgioGlobal.Core.Entities;
using PruebaUnoAgioGlobal.Infrastructure.Contexts;
using System.Linq;

namespace PruebaUnoAgioGlobal.Infrastructure.Migrations.Seedbeds
{
    public static class AirlinesSeedbed
    {
        public static void PutSeeds(AppDbContext context)
        {
            if (!context.Airlines.Any())
            {
                var airlinesToInsert = new Airline[]
                {
                    new Airline()
                    {
                        AirlineId = 1,
                        Code = "IB",
                        Name = "Iberia",
                    },
                    new Airline()
                    {
                        AirlineId = 2,
                        Code = "UX",
                        Name = "Air Europa",                 
                    },
                    new Airline()
                    {
                        AirlineId = 3,
                        Code = "VY",
                        Name = "Vueling",
                    },
                    new Airline()
                    {
                        AirlineId = 4,
                        Code = "FR",
                        Name = "Ryanair",
                    },
                    new Airline()
                    {
                        AirlineId = 5,
                        Code = "KL",
                        Name = "KLM",
                    },
                };

                context.Airlines.AddRange(airlinesToInsert);

                context.SaveChanges();
            }
        }
    }
}
