using HPowerTunings.ViewModels.ClientModels;
using System.Threading.Tasks;

namespace HPowerTunings.Services.Client
{
    public interface ICustomClientService
    {
        Task<bool> CreateClientAsync(AdminRegisterClientOutputModel model);
    }
}
