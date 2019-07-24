using System.Collections.Generic;

namespace HPowerTunings.Data.Models
{
    public class CarBrand : BaseModel
    {
        public string Name { get; set; }


        public virtual ICollection<Car> Cars { get; set; }

        public virtual ICollection<CarModel> Models { get; set; }
    }
}
