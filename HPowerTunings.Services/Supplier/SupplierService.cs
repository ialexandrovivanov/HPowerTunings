using System.Threading.Tasks;
using HPowerTunings.Data;
using HPowerTunings.ViewModels.RepairModels;

namespace HPowerTunings.Services.Supplier
{
    public class SupplierService : ISupplierService
    {
        private readonly ApplicationDbContext context;

        public SupplierService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateSupplier(ProceedRepairModel model)
        {
            var supplier = new Data.Models.Supplier()
                           {
                               CompanyName = model.Out.SupplierName,
                               Email = model.Out.SupplierEmail,
                               PhoneNumber = model.Out.SupplierPhone,
                               Url = model.Out.SupplierUrl
                           };

            var result = await this.context.Suppliers.AddAsync(supplier);

            if (result != null)
            {
                await this.context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
