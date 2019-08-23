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
        public string CarBrandName { get; set; }
        [HiddenInput]
        public string CarModelName { get; set; }
        [HiddenInput]
        public string Id { get; set; }
    }
}
