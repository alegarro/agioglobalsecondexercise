using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PruebaUnoAgioGlobal.Core.BusinessServices;
using PruebaUnoAgioGlobal.Core.Entities;
using PruebaUnoAgioGlobal.Infrastructure.Contexts;
using PruebaUnoAgioGlobal.Infrastructure.Migrations.Seedbeds;
using PruebaUnoAgioGlobal.Infrastructure.Repositories;
using Xunit;

namespace PruebaUnoAgioGlobal.Tests.Integration.Data
{
    public class FlightsBusinessServiceShould
    {
        private readonly AppDbContext _dbContext;
        private readonly  Repository<Flight> _flightRepository;

        public FlightsBusinessServiceShould()
        {
            _dbContext = new AppDbContext(CreateNewContextOptions());
            _flightRepository = GetRepository();
        }

        [Fact]
        public async void ValidateFlightDistance()
        {
            var flightBusinessService = new FlightsBusinessService();

            var firstFlightTestDB = await _flightRepository.GetById(1);

            var firstFlightCalculatedDistance = flightBusinessService.GetFlightDistance(firstFlightTestDB);
            Assert.Equal(434, firstFlightCalculatedDistance);

            var secondFlightTestDB = await _flightRepository.GetById(2);

            var secondFlightCalculatedDistance = flightBusinessService.GetFlightDistance(secondFlightTestDB);
            Assert.Equal(2022, secondFlightCalculatedDistance);
        }

        [Fact]
        public async void ValidateFlightFuelComsuption()
        {
            var flightBusinessService = new FlightsBusinessService();

            var firstFlightTestDB = await _flightRepository.GetById(1);

            var firstFlightCalculatedFuelConsumption = flightBusinessService.GetFuelConsumption(firstFlightTestDB);
            Assert.Equal(3182, firstFlightCalculatedFuelConsumption);

            var secondFlightTestDB = await _flightRepository.GetById(2);

            var secondFlightCalculatedFuelConsumption = flightBusinessService.GetFuelConsumption(secondFlightTestDB);
            Assert.Equal(12710, secondFlightCalculatedFuelConsumption);
        }

        private static DbContextOptions<AppDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("FlightBusinessServiceTestingDB")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        private Repository<Flight> GetRepository()
        {            
            SeedDatabase.PopulateTestData(_dbContext);

            return new Repository<Flight>(_dbContext);
        }
    }
}