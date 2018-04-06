using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PruebaUnoAgioGlobal.Core.BusinessServices;
using PruebaUnoAgioGlobal.Core.Entities;
using PruebaUnoAgioGlobal.Core.Interfaces.Repositories;
using PruebaUnoAgioGlobal.Infrastructure.Contexts;
using PruebaUnoAgioGlobal.Infrastructure.Repositories;
using PruebaUnoAgioGlobal.Web.ApplicationServices;
using PruebaUnoAgioGlobal.Web.Mappers;
using PruebaUnoAgioGlobal.Web.ModelsApi.Flights.Requests;
using System;
using System.Linq;
using System.Threading;
using Xunit;

namespace PruebaUnoAgioGlobal.Tests.Integration.Data
{
    public class FlightsAplicationServiceShould
    {
        private readonly FlightsService _flightsService;
        private readonly Repository<Airport> _airportRepository;
        private readonly Repository<Airline> _airlineRepository;

        public FlightsAplicationServiceShould()
        {
            var dbContext = new AppDbContext(CreateNewContextOptions());

            _airportRepository = new Repository<Airport>(dbContext);
            _airlineRepository = new Repository<Airline>(dbContext);
            _flightsService = new FlightsService(GetRepository(dbContext), new FlightsBusinessService(), _airportRepository);

            // Avoid automapper registration twice, in order void an exception in tests launched in parallel
            // Open Bug in Automapper dependency injection (https://github.com/AutoMapper/AutoMapper.Extensions.Microsoft.DependencyInjection/issues/36) when run test in paralell, random sleep is a workaround that I did and it works!
            Thread.Sleep(new Random(DateTime.Now.Millisecond).Next(5000));
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.AddProfiles(new[] { typeof(FlightsProfile) }));
        }

        [Fact]
        public async void AddItemAndSetId()
        {
            // Insert Airline
            var airline = new Airline()
            {
                AirlineId = 1,
                Code = "IB",
                Name = "Iberia"
            };
            var insertedAirlineId = await _airlineRepository.Add(airline);

            // Insert Airports
            var airportOne = new Airport()
            {
                AirportId = 1,
                Code = "AGP",
                Name = "Málaga - Costa del Sol",
                Latitude = 36.674,
                Longitude = -4.449,
                Country = "Spain",
            };
            var insertedAirportOneId = await _airportRepository.Add(airportOne);

            var airportTwo = new Airport()
            {
                AirportId = 2,
                Code = "AGP",
                Name = "Málaga - Costa del Sol",
                Latitude = 36.674,
                Longitude = -4.449,
                Country = "Spain",
            };
            var insertedAirportTwoId = await _airportRepository.Add(airportTwo);

            // Insert Flight
            var insertFlightRequest = new CreateFlightRequest()
            {                
                AirlineId = 1,
                Code = Guid.NewGuid().ToString(),
                OriginAirportId = 1,
                DestinationAirportId = 2,
            };
            var insertedId = await _flightsService.Insert(insertFlightRequest);

            // Get first flight
            var flightsList = await _flightsService.GetAll();
            var newFlight = flightsList.FirstOrDefault();
            
            // Validate
            Assert.Equal(insertFlightRequest.Code, newFlight.Code);
            Assert.True(newFlight.FlightId > 0);
        }

        [Fact]
        public async void UpdateItemAfterAddingIt()
        {
            // Insert Airline
            var airline = new Airline()
            {
                AirlineId = 1,
                Code = "IB",
                Name = "Iberia"
            };
            var insertedAirlineId = await _airlineRepository.Add(airline);

            // Insert Airports
            var airportOne = new Airport()
            {
                AirportId = 1,
                Code = "AGP",
                Name = "Málaga - Costa del Sol",
                Latitude = 36.674,
                Longitude = -4.449,
                Country = "Spain",
            };
            var insertedAirportOneId = await _airportRepository.Add(airportOne);

            var airportTwo = new Airport()
            {
                AirportId = 2,
                Code = "AGP",
                Name = "Málaga - Costa del Sol",
                Latitude = 36.674,
                Longitude = -4.449,
                Country = "Spain",
            };
            var insertedAirportTwoId = await _airportRepository.Add(airportTwo);

            // Add a new flight
            var initialCode = Guid.NewGuid().ToString();
            var insertFlightRequest = new CreateFlightRequest()
            {
                AirlineId = 1,
                Code = initialCode,
                OriginAirportId = 1,
                DestinationAirportId = 2,
            };
            var insertedId = await _flightsService.Insert(insertFlightRequest);

            // Get inserted Flight
            var getFlightByIdRequest = new GetFlightByIdRequest()
            {
                FlightId = insertedId
            };
            var insertedFlight = await _flightsService.GetById(getFlightByIdRequest);
            
            // Change Code
            var newCode = Guid.NewGuid().ToString();
            insertedFlight.Code = newCode;

            // Update the flight
            var flightUpdateRequest = Mapper.Map<UpdateFlightRequest>(insertFlightRequest);
            flightUpdateRequest.FlightId = insertedId;
            await _flightsService.Update(flightUpdateRequest);

            // Validate updated item
            var updatedFlight = await _flightsService.GetById(getFlightByIdRequest);

            Assert.NotEqual(insertedFlight.Code, updatedFlight.Code);
            Assert.Equal(insertedFlight.FlightId, updatedFlight.FlightId);
        }

        [Fact]
        public async void DeleteItemAfterAddingIt()
        {            
            // Insert Airline
            var airline = new Airline()
            {
                AirlineId = 1,
                Code = "IB",
                Name = "Iberia"
            };
            var insertedAirlineId = await _airlineRepository.Add(airline);

            // Insert Airports
            var airportOne = new Airport()
            {
                AirportId = 1,
                Code = "AGP",
                Name = "Málaga - Costa del Sol",
                Latitude = 36.674,
                Longitude = -4.449,
                Country = "Spain",
            };
            var insertedAirportOneId = await _airportRepository.Add(airportOne);

            var airportTwo = new Airport()
            {
                AirportId = 2,
                Code = "AGP",
                Name = "Málaga - Costa del Sol",
                Latitude = 36.674,
                Longitude = -4.449,
                Country = "Spain",
            };
            var insertedAirportTwoId = await _airportRepository.Add(airportTwo);

            // Add a new flight
            var initialCode = Guid.NewGuid().ToString();
            var insertFlightRequest = new CreateFlightRequest()
            {
                AirlineId = 1,
                Code = initialCode,
                OriginAirportId = 1,
                DestinationAirportId = 2,
            };
            var insertedId = await _flightsService.Insert(insertFlightRequest);

            // Delete the flight
            var deleteFlightRequest = new DeleteFlightRequest()
            {
                FlightId = insertedId
            };            
            await _flightsService.Delete(deleteFlightRequest);

            // Get the flights list
            var flightsList = await _flightsService.GetAll();

            // Verify is not present the flight
            Assert.DoesNotContain(flightsList, f => f.Code == initialCode);
        }

        private IFlightsRepository GetRepository(AppDbContext dbContext)
        {            
            return new FlightsRepository(dbContext);
        }

        private static DbContextOptions<AppDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("FlightsServiceTestingDB")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}