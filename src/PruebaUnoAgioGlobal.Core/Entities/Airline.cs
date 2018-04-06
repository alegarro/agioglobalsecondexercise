using PruebaUnoAgioGlobal.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaUnoAgioGlobal.Core.Entities
{
    /// <summary>
    /// Entity that describes an airline.
    /// </summary>
    [Table("Airlines")]
    public class Airline : Entity
    {
        [Key]
        public int AirlineId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; } 

        public override string GetIdPropertyInfo()
        {
            return nameof(AirlineId);
        }
    }
}