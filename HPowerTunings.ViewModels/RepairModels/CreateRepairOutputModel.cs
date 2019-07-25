using HPowerTunings.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.ViewModels.RepairModels
{
    public class CreateRepairOutputModel
    {
        [Required]
        [IsRegNumberExists]
        public string CarRegNumber { get; set; }
        public string Description { get; set; }
        public string RepairName { get; set; }
        public decimal RepairPrice { get; set; }
        public DateTime? StartedOn { get; set; } = DateTime.Now;
        public DateTime? FinishedOn { get; set; }
        public string Mechanic1FullName { get; set; }
        public string Mechanic2FullName { get; set; }
        public ICollection<string> AllMechanicNames { get; set; }
    }
}
