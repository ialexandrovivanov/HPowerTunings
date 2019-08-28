using HPowerTunings.Services.Client;
using HPowerTunings.ViewModels.ClientModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPowerTunings.Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly ICustomClientService customClientService;

        public ClientController(ICustomClientService clientService)
        {
            this.customClientService = clientService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminCreateClient()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminCreateClient(AdminRegisterClientOutputModel model)
        {
            if (ModelState.IsValid)
            {
                await this.customClientService.CreateClientAsync(model, this.Url, this.Request.Scheme);
                return Redirect("/Client/SuccessCreate");
            }
           
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ClientStartEndStatistics(ClientStartEndOutputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await this.customClientService.GetAllClientsPeriodAsync(model);

            return View(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult SuccessCreate()
        {
            return View();
        }
    }
}