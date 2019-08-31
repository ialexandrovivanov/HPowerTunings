using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AutoMapper;
using HPowerTunings.Data;
using HPowerTunings.ViewModels.CarModels;
using HPowerTunings.ViewModels.ClientModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HPowerTunings.Services.Client
{
    public class CustomClientService : ICustomClientService
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        private readonly UserManager<Data.Models.Client> userManager;
        private readonly IEmailSender emailSender;
        private readonly IHttpContextAccessor httpContextAccessor;
        public CustomClientService(ApplicationDbContext context, 
                                   UserManager<Data.Models.Client> userManager,
                                   IEmailSender emailSender,
                                   IMapper mapper,
                                   IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
            this.userManager = userManager;
            this.context = context;
            this.emailSender = emailSender;
        }

        public async Task<bool> CreateClientAsync(AdminRegisterClientOutputModel model, IUrlHelper urlHelper, string requestScheme)
        {
            var user = new Data.Models.Client { UserName = model.UserName, Email = model.Email };
            var result = await this.userManager.CreateAsync(user, "123");
            var roleResult = await userManager.AddToRoleAsync(user, "User");

            if (result.Succeeded && roleResult.Succeeded)
            {
                var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = urlHelper.Page("/Account/ConfirmEmail",
                                               pageHandler: null,
                                               values: new { area = "Identity", userId = user.Id, code = code },
                                               protocol: requestScheme);

                await emailSender.SendEmailAsync(model.Email, "Confirm your email address",
                    $"Please confirm the existence of your email address by " +
                    $"<a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Clicking here</a>");

                return true;
            }

            return false;
        }

        public async Task<ICollection<ClientViewModel>> GetAllClientsPeriodAsync(ClientStartEndOutputModel model)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var adminEmail = this.context.Clients.FindAsync(userId).Result.Email;
            var clients = this.context
                               .Clients
                               .Where(c => c.CreatedOn.Value.Date >= model.StartDate &&
                                      c.CreatedOn.Value.Date <= model.EndDate &&
                                      c.Email != adminEmail && c.EmailConfirmed == true)
                               .Select(c => c)
                               .ToList();

            var result = new List<ClientViewModel>();
            foreach (var client in clients)
            {
                var res = new ClientViewModel();
                res.UserName = client.UserName;
                res.Email = client.Email;
                res.PhoneNumber = client.PhoneNumber;
                res.TotalMoneyPaid = this.context
                                         .Repairs
                                         .Where(r => r.Car.Client.Email == client.Email)
                                         .SelectMany(r => r.Car.Repairs)
                                         .Sum(x => x.RepairPrice);

                res.TotalRepairs = this.context
                                       .Repairs
                                       .Where(r => r.Car.Client.Email == client.Email)
                                       .Count();

                var cars = client.Cars
                                 .Select(c => mapper.Map<Data.Models.Car, ClientCarDetailsViewModel>(c))
                                 .ToList();

                res.Cars = cars;

                result.Add(res);
            }
            await Task.Delay(0);
            return result;
        }
    }
}
