using System.Threading.Tasks;
using AutoMapper;
using HPowerTunings.Data;
using HPowerTunings.ViewModels.RepairModels;

namespace HPowerTunings.Services.Supplier
{
    public class SupplierService : ISupplierService
    {
        private readonly IMapper mapper;

        private readonly ApplicationDbContext context;

        public SupplierService(ApplicationDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<bool> CreateSupplier(ProceedRepairModel model)
        {
            var supplier = mapper.Map<ProceedRepairModelOut, Data.Models.Supplier>(model.Out);

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
