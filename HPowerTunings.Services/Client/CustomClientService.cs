using System.Threading.Tasks;
using HPowerTunings.Data;
using HPowerTunings.ViewModels.ClientModels;

namespace HPowerTunings.Services.Client
{
    public class CustomClientService : ICustomClientService
    {
        private ApplicationDbContext context;
        public async Task<bool> CreateClientAsync(AdminRegisterClientOutputModel model)
        {
            await Task.Delay(0);
            throw new System.NotImplementedException();
        }
    }
}
