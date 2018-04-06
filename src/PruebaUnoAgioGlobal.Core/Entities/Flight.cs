using PruebaUnoAgioGlobal.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaUnoAgioGlobal.Core.Entities
{
    /// <summary>
    /// Entity that describes a flight.
    /// </summary>
    [Table("Flights")]
    public class Flight : Entity
    {
        [Key]
        public int FlightId { get; set; }
        public string Code { get; set; } 
        public int OriginAirportId { get; set; }
        [ForeignKey(nameof(OriginAirportId))]
        public Airport OriginAirport { get; set; }
        public int DestinationAirportId { get; set; }
        [ForeignKey(nameof(DestinationAirportId))]
        public Airport DestinationAirport { get; set; }
        public int AirlineId { get; set; }
        [ForeignKey(nameof(AirlineId))]
        public Airline Airline { get; set; }
        public int Distance { get; set; }
        public int FuelConsumption { get; set; }

        public override string GetIdPropertyInfo()
        {
            return nameof(FlightId);
        }
    }
}