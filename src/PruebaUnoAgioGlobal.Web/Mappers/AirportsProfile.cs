
using AutoMapper;
using PruebaUnoAgioGlobal.Core.Entities;
using PruebaUnoAgioGlobal.Web.ModelsApi.Airports.Responses;

namespace PruebaUnoAgioGlobal.Web.Mappers
{
    /// <summary>
    /// Profile for AutoMapper to map Airport entity.
    /// </summary>
    public class AirportsProfile : Profile
    {
        /// <summary>
        /// Generates a new instance of the class.
        /// </summary>
        public AirportsProfile()
        {
            CreateMap<Airport, AirportDTO>()
                .ForMember(opt => opt.CodeAndName, ext => ext.MapFrom(inp => $"{inp.Code} - {inp.Name}"));
        }
    }
}
