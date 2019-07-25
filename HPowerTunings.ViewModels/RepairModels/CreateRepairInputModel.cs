using System.Collections.Generic;

namespace HPowerTunings.ViewModels.RepairModels
{
    public class CreateRepairInputModel
    {
        ICollection<string> RegNumbers { get; set; }
        public string CarRegNumber { get; set; }
    }
}
