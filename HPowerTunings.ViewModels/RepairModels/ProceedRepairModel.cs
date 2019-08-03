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
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string RegNumber { get; set; }
        public string VinNumber { get; set; }
        public string RepairName { get; set; }
        public ICollection<string> Suppliers { get; set; }
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
        [Range(typeof(decimal), "0.01", "99999", ErrorMessage = "Insert valid amount")]
        public decimal PartPrice { get; set; }

        [Range(1, 10, ErrorMessage = "Insert valid rate 1 - 10")]
        public int RateThisDelivery { get; set; }
    }
}