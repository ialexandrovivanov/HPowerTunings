using System;

namespace HPowerTunings.ViewModels.PartModels
{
    public class PartStatisticsViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public DateTime? StartedOn { get; set; }
        public decimal Price { get; set; }
        public int ClientRating { get; set; }
    }
}
