using FluentValidation;
using PruebaUnoAgioGlobal.Web.ModelsApi.Flights.Requests;
using PruebaUnoAgioGlobal.Web.Resources;

namespace PruebaUnoAgioGlobal.Web.Models.Provincias.Validators
{
    /// <summary>
    /// Class that implements the validator for the GetFlightByIdRequest.
    /// </summary>
    public class GetFlightByIdRequestValidator : AbstractValidator<GetFlightByIdRequest>
    {
        /// <summary>
        /// Generates a new instance of the GetFlightByIdRequest validator.
        /// </summary>
        public GetFlightByIdRequestValidator()
        {
            RuleFor(r => r.FlightId).NotEmpty().WithMessage(string.Format(ValidationMessages.FieldCannotBeEmpty, nameof(GetFlightByIdRequest.FlightId)));
        }
    }
}
