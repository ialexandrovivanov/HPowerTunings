using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPowerTunings.Data.Models
{
    public class Car : BaseModel
    {
        public string RegistrationNumber { get; set; }
        public string Rama { get; set; }
        public int TraveledDistance { get; set; }

        [ForeignKey("CarBrand")]
        public string CarBrandId { get; set; }
        public virtual CarBrand CarBrand { get; set; }

        [ForeignKey("Client")]
        public string ClientId { get; set; }
        public virtual Client Client { get; set; }

        [ForeignKey("CarModel")]
        public string CarModelId { get; set; }
        public virtual CarModel CarModel { get; set; }

        public virtual ICollection<Repair> Repairs { get; set; } = new HashSet<Repair>();
    }
}