using PruebaUnoAgioGlobal.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaUnoAgioGlobal.Core.Entities
{
    /// <summary>
    /// Entity that describes an airport.
    /// </summary>
    [Table("Airports")]
    public class Airport : Entity
    {
        [Key]
        public int AirportId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; } 
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Country { get; set; }

        public override string GetIdPropertyInfo()
        {
            return nameof(AirportId);
        }
    }
}