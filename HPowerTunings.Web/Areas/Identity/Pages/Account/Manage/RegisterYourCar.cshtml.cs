using HPowerTunings.Data;
using HPowerTunings.Data.Models;
using HPowerTunings.ViewModels.CarModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HPowerTunings.Web.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    public class RegCarModel : PageModel
    {
        public InputModel Input { get; set; }
        public string  CarBrand;
        private ApplicationDbContext context;
        private  UserManager<Client> userManager;

        public RegCarModel(ApplicationDbContext context, UserManager<Client> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        
        public async Task<IActionResult> OnGetAsync()
        {
            var client = await this.userManager.GetUserAsync(User);
            var clientCars = client.Cars
                                   .Where(c => c.IsDeleted == false)
                                   .Select(c => new ClientCarsViewModel()
                                   {
                                       Id = c.Id,
                                       CarBrand = c.CarBrand.Name,
                                       CarModel = c.CarModel.Name
                                   })
                                   .ToList();

            Input = new InputModel()
            {
                CarBrands = this.context.CarBrands.Select(cb => cb.Name).ToList(),
                ClientCars = clientCars
            };

            await Task.Delay(0);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string carBrand)
        {
            if (carBrand == null)
                return Redirect("RegisterYourCar");

            await Task.Delay(0);
            return RedirectToAction("Index", "Car", new { carBrand = carBrand });
        }

        public class InputModel
        {
            public ICollection<string> CarBrands { get; set; }
            public ICollection<ClientCarsViewModel> ClientCars { get; set; }


        }
        public class OutputModel
        {
            [Required]
            [MinLength(1)]
            public string CarBrand { get; set; }
        }
    }
}