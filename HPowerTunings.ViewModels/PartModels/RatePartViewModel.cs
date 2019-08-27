using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.ViewModels.PartModels
{
    public class RatePartViewModel
    {
        public string Id { get; set; }
        public string RepairId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }

        [Range(1, 10, ErrorMessage = "Please provide number between 1 and 10")]
        public int Rate { get; set; }
    }
}
