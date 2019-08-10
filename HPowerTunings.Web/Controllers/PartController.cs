using HPowerTunings.Services.Part;
using HPowerTunings.ViewModels.RepairModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPowerTunings.Web.Controllers
{
    public class PartController : Controller
    {
        private readonly IPartService partService;

        public PartController(IPartService partService)
        {
            this.partService = partService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddPart(ProceedRepairModel model)
        {
            if (ModelState.IsValid)
            {
                if (await this.partService.AddPart(model))
                {
                    return Redirect($"/Repair/ProceedRepair/?id={model.In.RepairId}");
                }
            }

            return Redirect($"/Repair/ProceedRepair?id = {model.In.RepairId}");
        }
    }
}