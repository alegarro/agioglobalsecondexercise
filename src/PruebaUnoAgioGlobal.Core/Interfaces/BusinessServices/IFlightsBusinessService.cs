using PruebaUnoAgioGlobal.Core.Entities;

namespace PruebaUnoAgioGlobal.Core.Interfaces.BusinessServices
{
    /// <summary>
    /// Business Service Interface for flights.
    /// </summary>
    public interface IFlightsBusinessService
    {
        /// <summary>
        /// Return the flight distance beetween two airports using its coordinates.
        /// </summary>
        /// <param name="flight">Flight data.</param>
        /// <returns>Distance between the origin and destination airport.</returns>
        int GetFlightDistance(Flight flight);

        /// <summary>
        /// Return the flight fuel consumption beetween two airports using the flight distance.
        /// </summary>
        /// <param name="flight">Flight data.</param>
        /// <returns>Fuel Consumption in liters between the origin and destination airport.</returns>
        int GetFuelConsumption(Flight flight);
    }
}
