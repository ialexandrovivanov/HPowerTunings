using HPowerTunings.ViewModels.PartModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.ViewModels.RepairModels
{
    public class ProceedRepairModel
    {
        public In In { get; set; }
        public Out Out { get; set; }

        public ProceedRepairModel()
        {
            this.In = new In();
            this.Out = new Out();
        }
    }

    public class In
    {
        public string RepairId { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string RegNumber { get; set; }
        public string VinNumber { get; set; }
        public string RepairName { get; set; }
        public DateTime? StartedOn { get; set; }
        public int CountParts { get; set; }
        public string SumPartsPrices { get; set; }
        public ICollection<string> Suppliers { get; set; } = new List<string>();
        public ICollection<PartStatisticsViewModel> Parts { get; set; } = new List<PartStatisticsViewModel>();
    }
    public class Out
    {
        [Required(ErrorMessage = "Required")]
        public string PartName { get; set; }

        [Required(ErrorMessage ="Required")]
        public string PartManufacturer { get; set; }

        [Required(ErrorMessage = "Required")]
        public string PartSupplier { get; set; }

        [Required(ErrorMessage = "Required")]
        [Range(typeof(decimal), "0.01", "999999", ErrorMessage = "Insert valid amount")]
        public decimal PartPrice { get; set; }

        [Range(0, 10, ErrorMessage = "Insert valid rate 0 - 10")]
        public int RateThisDelivery { get; set; }

        [Range(typeof(Decimal), "0", "9999999")]
        public decimal RepairPrice { get; set; }

        [RegularExpression(@"^[a-zA-Zа-яА-Я 0-9]+")]
        public string SupplierName { get; set; }

        [Phone]
        public string SupplierPhone { get; set; }

        [EmailAddress]
        public string SupplierEmail { get; set; }

        [Url]
        public string SupplierUrl { get; set; }
    }
}