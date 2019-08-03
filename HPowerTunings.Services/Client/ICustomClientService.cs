using HPowerTunings.ViewModels.ClientModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPowerTunings.Services.Client
{
    public interface ICustomClientService
    {
        Task<bool> CreateClientAsync(AdminRegisterClientOutputModel model, IUrlHelper urlHelper, string requestScheme);
        Task<ICollection<ClientViewModel>> GetAllClientsPeriodAsync(ClientStartEndOutputModel model);
    }
}
