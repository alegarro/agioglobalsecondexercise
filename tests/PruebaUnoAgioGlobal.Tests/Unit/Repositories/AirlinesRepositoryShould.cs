using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PruebaUnoAgioGlobal.Core.Entities;
using PruebaUnoAgioGlobal.Infrastructure.Contexts;
using PruebaUnoAgioGlobal.Infrastructure.Repositories;
using System;
using System.Linq;
using System.Threading;
using Xunit;

namespace PruebaUnoAgioGlobal.Tests.Integration.Data
{
    public class AirlinesRepositoryShould
    {
        private readonly AppDbContext _dbContext;
        private readonly Repository<Airline> _airlineRepository;

        public AirlinesRepositoryShould()
        {
            _dbContext = new AppDbContext(CreateNewContextOptions());
            _airlineRepository = GetRepository();

            // Open Bug in Automapper dependency injection (https://github.com/AutoMapper/AutoMapper.Extensions.Microsoft.DependencyInjection/issues/36) when run test in paralell, random sleep is a workaround that I did and it works!
            Thread.Sleep(new Random(DateTime.Now.Millisecond).Next(5000));
        }

        [Fact]
        public async void AddItemAndSetId()
        {           
            var airline = new Airline();

            await _airlineRepository.Add(airline);

            var airlineList = await _airlineRepository.List();
            var newAirline = airlineList.FirstOrDefault();

            Assert.Equal(airline, newAirline);
            Assert.True(newAirline.AirlineId > 0);
        }

        [Fact]
        public async void UpdateItemAfterAddingIt()
        {
            // Add a new airline
            var initialCode = Guid.NewGuid().ToString();
            var airlineItem = new Airline()
            {
                Code = initialCode
            };
            var insertedId = await _airlineRepository.Add(airlineItem);

            // Detach the item so we get a different instance
            _dbContext.Entry(airlineItem).State = EntityState.Detached;

            // Get the airlines list, get the inserted element and update its title
            var airlinesList = await _airlineRepository.List();
            var newAirline = airlinesList.FirstOrDefault(i => i.Code == initialCode);
            Assert.NotSame(airlineItem, newAirline);

            var newCode = Guid.NewGuid().ToString();
            newAirline.Code = newCode;

            // Update the airline
            await _airlineRepository.Update(newAirline);

            // Validate updated item
            airlinesList = await _airlineRepository.List();
            var updatedAirline = airlinesList.FirstOrDefault(i => i.Code == newCode);

            Assert.NotEqual(airlineItem.Code, updatedAirline.Code);
            Assert.Equal(newAirline.AirlineId, updatedAirline.AirlineId);
        }

        [Fact]
        public async void DeleteItemAfterAddingIt()
        {
            // Add a new airline
            var initialCode = Guid.NewGuid().ToString();
            var airlineItem = new Airline()
            {
                Code = initialCode
            };
            var insertedId = await _airlineRepository.Add(airlineItem);

            // Delete the airline
            await _airlineRepository.Delete(airlineItem);

            // Get the airlines list
            var airlinesList = await _airlineRepository.List();

            // Verify is not present the airline
            Assert.DoesNotContain(airlinesList, f => f.Code == initialCode);
        }

        private Repository<Airline> GetRepository()
        {
            return new Repository<Airline>(_dbContext);
        }

        private static DbContextOptions<AppDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("ArilineTestingDB")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}