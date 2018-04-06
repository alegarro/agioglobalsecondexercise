
using AutoMapper;
using PruebaUnoAgioGlobal.Core.Entities;
using PruebaUnoAgioGlobal.Web.ModelsApi.Flights.Requests;
using PruebaUnoAgioGlobal.Web.ModelsApi.Flights.Responses;

namespace PruebaUnoAgioGlobal.Web.Mappers
{
    /// <summary>
    /// Profile for AutoMapper to map Flights entity.
    /// </summary>
    public class FlightsProfile : Profile
    {
        /// <summary>
        /// Generates a new instance of the class.
        /// </summary>
        public FlightsProfile()
        {
            CreateMap<Flight, FlightDTO>();
            CreateMap<Flight, FlightListDTO>()
                .ForMember(opt => opt.OriginAirport, ext => ext.MapFrom(inp => $"{inp.OriginAirport.Code} - {inp.OriginAirport.Name}"))
                .ForMember(opt => opt.DestinationAirport, ext => ext.MapFrom(inp => $"{inp.DestinationAirport.Code} - {inp.DestinationAirport.Name}"))
                .ForMember(opt => opt.Airline, ext => ext.MapFrom(inp => $"{inp.Airline.Code} - {inp.Airline.Name}"));
            CreateMap<CreateFlightRequest, Flight>()
                .ReverseMap();
            CreateMap<UpdateFlightRequest, Flight>()
                .ReverseMap();
            CreateMap<FlightListDTO, CreateFlightRequest>();
            CreateMap<CreateFlightRequest, UpdateFlightRequest>();
        }
    }
}
