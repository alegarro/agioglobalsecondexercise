using FluentValidation;
using PruebaUnoAgioGlobal.Web.ModelsApi.Flights.Requests;
using PruebaUnoAgioGlobal.Web.Resources;

namespace PruebaUnoAgioGlobal.Web.Models.Provincias.Validators
{
    /// <summary>
    /// Class that implements the validator for the DeleteFlightRequest.
    /// </summary>
    public class DeleteFlightRequestValidator : AbstractValidator<DeleteFlightRequest>
    {
        /// <summary>
        /// Generates a new instance of the DeleteFlightRequest validator.
        /// </summary>
        public DeleteFlightRequestValidator()
        {
            RuleFor(r => r.FlightId).NotEmpty().WithMessage(string.Format(ValidationMessages.FieldCannotBeEmpty, nameof(DeleteFlightRequest.FlightId)));
        }
    }
}
