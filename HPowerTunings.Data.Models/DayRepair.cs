using System.ComponentModel.DataAnnotations.Schema;

namespace HPowerTunings.Data.Models
{
    public class DayRepair
    {
        public string DayId { get; set; }
        public virtual Day Day { get; set; }

        [ForeignKey("Repair")]
        public string RepairId { get; set; }
        public virtual Repair Repair { get; set; }
    }
}