using HPowerTunings.Services.Supplier;
using HPowerTunings.ViewModels.RepairModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPowerTunings.Web.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            this.supplierService = supplierService;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSupplier(ProceedRepairModel model)
        {
            if (string.IsNullOrEmpty(model.Out.SupplierName))
            {
                return Redirect($"/Repair/ProceedRepair/?id={model.In.Id}");
            }

            if (await this.supplierService.CreateSupplier(model))
            {
                return Redirect($"/Repair/ProceedRepair/?id={model.In.Id}");
            }

            return Redirect($"/Repair/ProceedRepair/?id={model.In.Id}");
        }
    }
}