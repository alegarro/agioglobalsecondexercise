namespace PruebaUnoAgioGlobal.Web.ModelsApi.Flights.Requests
{
    public class CreateFlightRequest
    {       
        public string Code { get; set; }
        public int OriginAirportId { get; set; }
        public int DestinationAirportId { get; set; }
        public int AirlineId { get; set; }
    }
}
