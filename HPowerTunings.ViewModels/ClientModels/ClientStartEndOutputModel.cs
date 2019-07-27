using System;
using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.ViewModels.ClientModels
{
    public class ClientStartEndOutputModel
    {
        private const string RequiredErrorMessage = "Insert correct date and time";

        [Required(ErrorMessage = RequiredErrorMessage)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public DateTime EndDate { get; set; }
    }
}
