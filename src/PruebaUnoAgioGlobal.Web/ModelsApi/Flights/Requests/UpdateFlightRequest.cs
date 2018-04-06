using Newtonsoft.Json;

namespace PruebaUnoAgioGlobal.Web.ModelsApi.Flights.Requests
{
    public class UpdateFlightRequest
    {
        [JsonIgnore]
        public int FlightId { get; set; }
        public string Code { get; set; }
        public int OriginAirportId { get; set; }
        public int DestinationAirportId { get; set; }
        public int AirlineId { get; set; }
    }
}
