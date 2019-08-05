using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPowerTunings.Data.Models
{
    public class Part : BaseModel
    {
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public int ClientRating { get; set; }
      

        [ForeignKey("Supplier")]
        public string SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        [ForeignKey("Repair")]
        public string RepairId { get; set; }
        public virtual Repair Repair { get; set; }
    }
}
