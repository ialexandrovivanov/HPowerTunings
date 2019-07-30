using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.ViewModels.ClientModels
{
    public class AdminRegisterClientOutputModel
    {
        [Required(ErrorMessage = "Required")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        public string UserName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^[a-zA-Zа-яА-Я ]+$")]
        public string FullName { get; set; }
    }
}
