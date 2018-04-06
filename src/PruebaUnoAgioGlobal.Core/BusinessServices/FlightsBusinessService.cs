using PruebaUnoAgioGlobal.Core.Entities;
using PruebaUnoAgioGlobal.Core.Interfaces.BusinessServices;
using System;

namespace PruebaUnoAgioGlobal.Core.BusinessServices
{
    /// <summary>
    /// Business Service for flights.
    /// </summary>
    public class FlightsBusinessService : IFlightsBusinessService
    {
        private const double PIx = 3.141592653589793;
        private const double RADIUS = 6378.16;
        private const int FUEL_CONSUMPTION_IN_TAKE_OFF = 578;
        private const int FUEL_CONSUMPTION_PER_KILOMETER = 6;

        /// <summary>
        /// Return the flight distance beetween two airports using its coordinates.
        /// </summary>
        /// <param name="flight">Flight data.</param>
        /// <returns>Distance between the origin and destination airport.</returns>
        public int GetFlightDistance(Flight flight)
        {
            var latitudeDiference = GetRadians(flight.OriginAirport.Latitude - flight.DestinationAirport.Latitude);
            var longitudeDiference = GetRadians(flight.OriginAirport.Longitude - flight.DestinationAirport.Longitude);

            var angle = 
                (Math.Sin(latitudeDiference / 2) * Math.Sin(latitudeDiference / 2)) + Math.Cos(GetRadians(flight.DestinationAirport.Longitude)) * Math.Cos(GetRadians(flight.OriginAirport.Longitude)) * (Math.Sin(longitudeDiference / 2) * Math.Sin(longitudeDiference / 2));

            var squaredAngle = 2 * Math.Atan2(Math.Sqrt(angle), Math.Sqrt(1 - angle));

            return Convert.ToInt32(squaredAngle * RADIUS);
        }

        /// <summary>
        /// Return the flight fuel consumption beetween two airports using the flight distance.
        /// </summary>
        /// <param name="flight">Flight data.</param>
        /// <returns>Fuel Consumption in liters between the origin and destination airport.</returns>
        public int GetFuelConsumption(Flight flight)
        {
            var flightDistance = GetFlightDistance(flight);

            return FUEL_CONSUMPTION_IN_TAKE_OFF + flightDistance * FUEL_CONSUMPTION_PER_KILOMETER;
        }

        private static double GetRadians(double degrees)
        {
            return degrees * PIx / 180;
        }
    }
}
