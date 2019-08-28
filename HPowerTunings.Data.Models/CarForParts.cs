using System.Collections.Generic;

namespace HPowerTunings.Data.Models
{
    public class CarForParts : BaseModel
    {
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string RegistrationNumber { get; set; }
        public string Rama { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }

        public virtual ICollection<PartFromCar> PartsFromCar { get; set; }
    }
}
