using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.ViewModels.EmailModels
{
    public class SendEmailViewModel
    {
        [Required]
        public string CarModel { get; set; }

        [Required]
        public string Message { get; set; }
        public string Subject { get; set; }
        public string Picture { get; set; }
    }
}
