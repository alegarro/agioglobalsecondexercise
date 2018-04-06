namespace PruebaUnoAgioGlobal.Web.ModelsApi.Flights.Responses
{
    public class FlightDTO
    {
        public int FlightId { get; set; }
        public string Code { get; set; }
        public int OriginAirportId { get; set; }
        public int DestinationAirportId { get; set; }
        public int AirlineId { get; set; }
        public int Distance { get; set; }
        public int FuelConsumption { get; set; }
    }
}
