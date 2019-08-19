using HPowerTunings.Data;
using HPowerTunings.ViewModels.RepairModels;
using System.Linq;
using System.Threading.Tasks;

namespace HPowerTunings.Services.Part
{
    public class PartService : IPartService
    {
        private readonly ApplicationDbContext context;

        public PartService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddPart(ProceedRepairModel model)
        {
            var supplier = this.context.Suppliers.FirstOrDefault(s => s.SupplierName == model.Out.PartSupplier);
            var repair = this.context.Repairs.FirstOrDefault(r => r.Id == model.In.Id);
            var part = new Data.Models.Part()
            {
                Name = model.Out.PartName,
                Brand = model.Out.PartManufacturer,
                Price = model.Out.PartPrice,
                Supplier = supplier,
                SupplierId = supplier.Id,
                Repair = repair,
                RepairId = repair.Id,
            };

            await this.context.Parts.AddAsync(part);
            var result = this.context.SaveChanges();

            if (result == 1)
            {
                return true;
            }

            return false;
        }
    }
}
