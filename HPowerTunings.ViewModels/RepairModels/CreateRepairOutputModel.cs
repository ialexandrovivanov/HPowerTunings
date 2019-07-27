using HPowerTunings.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.ViewModels.RepairModels
{
    public class CreateRepairOutputModel
    {
        private const string regNumberError = "Use capital letters, numbers and correct length";

        [Required]
        [IsRegNumberExists]
        [RegularExpression("^[ABCEHKLMNOPTXY]{1,2}[0-9]{4,6}[ABCEHKLMNOPTXY]{1,2}$", ErrorMessage = regNumberError)]
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
