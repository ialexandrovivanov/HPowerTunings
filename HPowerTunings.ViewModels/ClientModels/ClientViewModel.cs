using HPowerTunings.ViewModels.CarModels;
using System.Collections.Generic;

namespace HPowerTunings.ViewModels.ClientModels
{
    public class ClientViewModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }    
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<ClientCarDetailsViewModel> Cars { get; set; } = new HashSet<ClientCarDetailsViewModel>();
        public int TotalRepairs { get; set; }
        public decimal TotalMoneyPaid { get; set; }
    }
}
