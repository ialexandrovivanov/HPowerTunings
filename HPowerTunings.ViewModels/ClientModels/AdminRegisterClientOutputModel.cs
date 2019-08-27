using HPowerTunings.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.ViewModels.ClientModels
{
    public class AdminRegisterClientOutputModel
    {
        [Required(ErrorMessage = "Required")]
        [EmailAddress]
        [IsEmailTaken]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        [IsUsernameTaken]
        [IsUsernameCorrect]
        public string UserName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
    }
}
