using System;
using System.Collections.Generic;

namespace HPowerTunings.ViewModels.RepairModels
{
    public class PendingRepairViewModel
    {
        public string RepairId { get; set; }
        public string CarModel { get; set; }
        public string RegNumber { get; set; }
        public string CarBrand { get; set; }
        public string ClientUserName { get; set; }
        public string ClientEmail { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string RepairName { get; set; }
        public string RepairDescription { get; set; }
        public string StartDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public ICollection<string> Mechanics { get; set; }
    }
}
