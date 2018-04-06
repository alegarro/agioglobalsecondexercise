
using AutoMapper;
using PruebaUnoAgioGlobal.Core.Entities;
using PruebaUnoAgioGlobal.Web.ModelsApi.Airlines.Responses;

namespace PruebaUnoAgioGlobal.Web.Mappers
{
    /// <summary>
    /// Profile for AutoMapper to map Airline entity.
    /// </summary>
    public class AirlinesProfile : Profile
    {
        /// <summary>
        /// Generates a new instance of the class.
        /// </summary>
        public AirlinesProfile()
        {
            CreateMap<Airline, AirlineDTO>()
                .ForMember(opt => opt.CodeAndName, ext => ext.MapFrom(inp => $"{inp.Code} - {inp.Name}"));
        }
    }
}
