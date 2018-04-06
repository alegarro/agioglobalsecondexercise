using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using PruebaUnoAgioGlobal.Core.Entities;
using PruebaUnoAgioGlobal.Core.Exceptions;
using PruebaUnoAgioGlobal.Core.Interfaces.BusinessServices;
using PruebaUnoAgioGlobal.Core.Interfaces.Repositories;
using PruebaUnoAgioGlobal.Web.ApplicationServices.Interfaces;
using PruebaUnoAgioGlobal.Web.ModelsApi.Flights.Requests;
using PruebaUnoAgioGlobal.Web.ModelsApi.Flights.Responses;
using PruebaUnoAgioGlobal.Web.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaUnoAgioGlobal.Web.ApplicationServices
{
    /// <summary>
    /// Flights Application Service.
    /// </summary>
    public class FlightsService : IFlightsService
    {
        private readonly IFlightsRepository _flightsRepository;
        private readonly IFlightsBusinessService _flightsBusinessService;
        private readonly IRepository<Airport> _airportsRepository;

        /// <summary>
        /// Generates a new instance of the flights application service.
        /// </summary>
        /// <param name="flightsRepository">Flights Repository Instance.</param>
        /// <param name="flightsBusinessService">Flights Business Service Instance</param>
        /// <param name="airportsRepository">Airports Repository Instance.</param>
        public FlightsService(IFlightsRepository flightsRepository, IFlightsBusinessService flightsBusinessService, IRepository<Airport> airportsRepository)
        {
            _flightsRepository = flightsRepository;
            _flightsBusinessService = flightsBusinessService;
            _airportsRepository = airportsRepository;
        }

        /// <summary>
        /// Returns a list of the flights stored in the system.
        /// </summary>
        /// <returns>List of the flights.</returns>
        public async Task<List<FlightListDTO>> GetAll()
        {
            var flightsEntityList = await _flightsRepository.GetAllFlights();

            return await Task.FromResult(Mapper.Map<List<FlightListDTO>>(flightsEntityList));
        }

        /// <summary>
        /// Gets a flight by Id.
        /// </summary>
        /// <param name="request">Request with the id of the flight to get.</param>
        /// <returns>Flight data.</returns>
        public async Task<FlightDTO> GetById(GetFlightByIdRequest request)
        {
            var flightEntity = await _flightsRepository.GetFlightById(request.FlightId);

            if (flightEntity == null)
            {
                throw new NotFoundException();
            }

            return await Task.FromResult(Mapper.Map<FlightDTO>(flightEntity));
        }

        /// <summary>
        /// Inserts a new flight in the database.
        /// </summary>
        /// <param name="request">Flight data to insert in the database.</param>
        public async Task<int> Insert(CreateFlightRequest request)
        {
            var createFlightEntity = Mapper.Map<Flight>(request);

            createFlightEntity.OriginAirport = await _airportsRepository.GetById(createFlightEntity.OriginAirportId);
            createFlightEntity.DestinationAirport = await _airportsRepository.GetById(createFlightEntity.DestinationAirportId);

            createFlightEntity.FuelConsumption = _flightsBusinessService.GetFuelConsumption(createFlightEntity);
            createFlightEntity.Distance = _flightsBusinessService.GetFlightDistance(createFlightEntity);

            var createdFlight = await _flightsRepository.Add(createFlightEntity);

            return await Task.FromResult(createFlightEntity.FlightId);
        }

        /// <summary>
        /// Updates a flight in the database.
        /// </summary>
        /// <param name="request">Flight data to update in the database.</param>
        public async Task Update(UpdateFlightRequest request)
        {
            if (request.FlightId <= 0)
            {
                throw new ValidationException(new List<ValidationFailure>()
                {
                    new ValidationFailure(nameof(request.FlightId), string.Format(ValidationMessages.FieldCannotBeEmpty, nameof(request.FlightId)))
                });
            }

            var currentFlightEntity = await _flightsRepository.GetById(request.FlightId);

            if (currentFlightEntity == null)
            {
                throw new NotFoundException();
            }
            
            var updateFlightEntity = currentFlightEntity = Mapper.Map<UpdateFlightRequest, Flight>(request, currentFlightEntity);

            updateFlightEntity.OriginAirport = await _airportsRepository.GetById(updateFlightEntity.OriginAirportId);
            updateFlightEntity.DestinationAirport = await _airportsRepository.GetById(updateFlightEntity.DestinationAirportId);

            updateFlightEntity.FuelConsumption = _flightsBusinessService.GetFuelConsumption(updateFlightEntity);
            updateFlightEntity.Distance = _flightsBusinessService.GetFlightDistance(updateFlightEntity);

            await _flightsRepository.Update(updateFlightEntity);
        }

        /// <summary>
        /// Deletes a flight in the database.
        /// </summary>
        /// <param name="request">Request with the id of the flight to delete.</param>
        public async Task Delete(DeleteFlightRequest request)
        {
            var currentFlightEntity = await _flightsRepository.GetById(request.FlightId);

            if (currentFlightEntity == null)
            {
                throw new NotFoundException();
            }

            await _flightsRepository.Delete(currentFlightEntity);
        }
    }
}
