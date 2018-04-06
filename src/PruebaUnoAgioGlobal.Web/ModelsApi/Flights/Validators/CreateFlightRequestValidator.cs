using FluentValidation;
using PruebaUnoAgioGlobal.Web.ModelsApi.Flights.Requests;
using PruebaUnoAgioGlobal.Web.Resources;

namespace FichaObraMobileAPI.Web.Models.Provincias.Validators
{
    /// <summary>
    /// Class that implements the validator for the UpdateFlightRequest.
    /// </summary>
    public class CreateFlightRequestValidator : AbstractValidator<CreateFlightRequest>
    {
        /// <summary>
        /// Generates a new instance of the CreateFlightRequest validator.
        /// </summary>
        public CreateFlightRequestValidator()
        {
            RuleFor(r => r.Code).NotEmpty().WithMessage(string.Format(ValidationMessages.FieldCannotBeEmpty, nameof(UpdateFlightRequest.Code)));
            RuleFor(r => r.AirlineId).NotEmpty().WithMessage(string.Format(ValidationMessages.FieldCannotBeEmpty, nameof(UpdateFlightRequest.AirlineId)));
            RuleFor(r => r.OriginAirportId).NotEmpty().WithMessage(string.Format(ValidationMessages.FieldCannotBeEmpty, nameof(UpdateFlightRequest.OriginAirportId)));
            RuleFor(r => r.DestinationAirportId).NotEmpty().WithMessage(string.Format(ValidationMessages.FieldCannotBeEmpty, nameof(UpdateFlightRequest.DestinationAirportId)));
        }
    }
}
