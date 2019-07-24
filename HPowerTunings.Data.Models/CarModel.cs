using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPowerTunings.Data.Models
{
    public class CarModel : BaseModel
    {
        public string Name { get; set; }

        [ForeignKey("CarBrand")]
        public string CarBrandId { get; set; }
        public virtual CarBrand CarBrand { get; set; }

        public virtual ICollection<Car> Car { get; set; }
    }
}