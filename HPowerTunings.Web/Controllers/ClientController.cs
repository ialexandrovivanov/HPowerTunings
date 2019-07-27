﻿using HPowerTunings.Services.Client;
using HPowerTunings.ViewModels.ClientModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
                await this.customClientService.CreateClientAsync(model);
                return Redirect("/Client/SuccessCreate");
            }
            return View(model);
        }
    }
}