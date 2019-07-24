using HPowerTunings.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.ViewModels.CarModels
{
    public class DeleteYourCarModel
    {
        [Required]
        [IsCurrentPassword]
        public string Password { get; set; }
        [HiddenInput]
        public string CarBrand { get; set; }
        [HiddenInput]
        public string CarModel { get; set; }
        [HiddenInput]
        public string CarId { get; set; }
    }
}
