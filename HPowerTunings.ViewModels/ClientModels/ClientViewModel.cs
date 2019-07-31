using HPowerTunings.ViewModels.CarModels;
using System.Collections.Generic;

namespace HPowerTunings.ViewModels.ClientModels
{
    public class ClientViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        ICollection<ClientCarDetailsViewModel> Cars { get; set; }

        public decimal TotalMoneyPaid { get; set; }
    }
}
