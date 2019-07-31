using HPowerTunings.ViewModels.ClientModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPowerTunings.Services.Client
{
    public interface ICustomClientService
    {
        Task<bool> CreateClientAsync(AdminRegisterClientOutputModel model);
        Task<ICollection<ClientViewModel>> GetAllClientsPeriodAsync(ClientStartEndOutputModel model);
    }
}
