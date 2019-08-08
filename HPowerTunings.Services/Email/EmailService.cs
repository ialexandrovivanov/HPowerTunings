using HPowerTunings.Data;
using HPowerTunings.ViewModels.EmailModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPowerTunings.Common.Email;

namespace HPowerTunings.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly ApplicationDbContext context;
        private readonly ICustomEmailSender emailSender;

        public EmailService(ApplicationDbContext context, ICustomEmailSender emailSender)
        {
            this.emailSender = emailSender;
            this.context = context;
        }

        public async Task<List<string>> GetAllModels()
        {
            var models = this.context.CarModels
                                     .Where(m => m.CarBrand.Name == "BMW")
                                     .Select(m => m.Name)
                                     .ToList();

            models.AddRange(this.context.CarModels
                                        .Where(m => m.CarBrand.Name == "Mini Cooper")
                                        .Select(m => m.Name)
                                        .ToList());

            models.AddRange(this.context.CarModels
                                        .Where(m => m.CarBrand.Name == "Range Rover")
                                        .Select(m => m.Name)
                                        .ToList());
            await Task.Delay(0);
            return models;
        }

        public async Task<bool> SendEmailsAsync(SendEmailViewModel model)
        {
            var allEmails = new List<string>();
            if (model.CarModel == "All Clients")
            {
                allEmails = this.context.Clients.Select(c => c.Email).ToList();
            }
            else
            {
                allEmails = this.context.Cars
                                        .Where(c => c.CarModel.Name == model.CarModel && c.IsDeleted == false)
                                        .Select(c => c.Client.Email)
                                        .ToList();
            }                           

            foreach (var email in allEmails)
            {
                await this.emailSender.SendEmailsAsync(email, model.Subject, model.Picture, model.Message);
            }

            return true;
        }
    }
}
