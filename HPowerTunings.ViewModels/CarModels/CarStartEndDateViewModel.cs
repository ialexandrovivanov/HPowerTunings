﻿using System;
using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.ViewModels.CarModels
{
    public class CarStartEndDateViewModel
    {
        private const string RequiredErrorMessage = "Insert correct date and time";

        [Required(ErrorMessage = RequiredErrorMessage)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public DateTime EndDate { get; set; }
    }
}
