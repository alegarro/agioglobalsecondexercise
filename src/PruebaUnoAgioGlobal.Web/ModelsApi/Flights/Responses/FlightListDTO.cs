namespace PruebaUnoAgioGlobal.Web.ModelsApi.Flights.Responses
{
    public class FlightListDTO
    {
        public int FlightId { get; set; }
        public string Code { get; set; }
        public string OriginAirport { get; set; }
        public string DestinationAirport { get; set; }
        public string Airline { get; set; }
        public int Distance { get; set; }
        public int FuelConsumption { get; set; }
    }
}
