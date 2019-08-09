using System;
using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.ViewModels.EmployeeModels
{
    public class EmployeeRegisterViewModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Zа-яА-Я ]+$", ErrorMessage = "Use letters and spaces")]
        public string FullName { get; set; }
        [Required]
        [RegularExpression(@"^[0-9 ]+$", ErrorMessage = "Use numbers and spaces")]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Zа-яА-Я 0-9]+$", ErrorMessage ="Use letters numbers and spaces")]
        public string Possition { get; set; }

        [RegularExpression(@"^[a-zA-Zа-яА-Я 0-9.,]+$", ErrorMessage = "Use letters numbers spaces dots and commas")]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public DateTime? HiredDate { get; set; } = DateTime.Now;
        public DateTime? FiredDate { get; set; }
    }
}
