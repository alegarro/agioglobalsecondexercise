using Microsoft.Extensions.DependencyInjection;
using PruebaUnoAgioGlobal.Infrastructure.Contexts;
using System;

namespace PruebaUnoAgioGlobal.Infrastructure.Migrations.Seedbeds
{
    public static class SeedDatabase
    {
        public static void PopulateTestData(AppDbContext context)
        {
            try
            {
                AirlinesSeedbed.PutSeeds(context);
                AirportSeedbed.PutSeeds(context);
                FlightsSeedbed.PutSeeds(context);                   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void PopulateProductionData(IServiceProvider applicationServices)
        {
            using (var serviceScope = applicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                try
                {
                    context.Database.BeginTransaction();

                    AirlinesSeedbed.PutSeeds(context);
                    AirportSeedbed.PutSeeds(context);
                    FlightsSeedbed.PutSeeds(context);

                    context.Database.CommitTransaction();
                }
                catch (Exception ex)
                {
                    context.Database.RollbackTransaction();
                }
            }
        }
    }
}