using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPowerTunings.Data.Models
{
    public class PartFromCar : BaseModel
    {
        public decimal Price { get; set; }
        public string Name { get; set; }
        public DateTime? SaledOn { get; set; } = DateTime.Now;

        [ForeignKey("CarForParts")]
        public string CarForPartsId { get; set; }
        public virtual CarForParts CarForParts { get; set; }
    }
}
