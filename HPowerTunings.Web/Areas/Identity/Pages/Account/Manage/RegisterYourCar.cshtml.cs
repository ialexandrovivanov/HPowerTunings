using AutoMapper;
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
        private readonly ApplicationDbContext context;
        private readonly UserManager<Client> userManager;
        private readonly IMapper mapper;

        public RegCarModel(ApplicationDbContext context, 
                           UserManager<Client> userManager,
                           IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
            this.userManager = userManager;
        }
        
        public async Task<IActionResult> OnGetAsync()
        {
            var client = await this.userManager.GetUserAsync(User);
            var clientCars = client.Cars
                                   .Where(c => c.IsDeleted == false)
                                   .Select(c => mapper.Map<Car, ClientCarsViewModel>(c))
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