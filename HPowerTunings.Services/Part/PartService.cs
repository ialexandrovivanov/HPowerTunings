using HPowerTunings.Data;
using HPowerTunings.ViewModels.RepairModels;
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

        public async Task<bool> CreatePart(ProceedRepairModel model)
        {
            var part = new Data.Models.Part()
            {
                Brand = model.Out.PartSupplier,
                Name = model.Out.PartSupplier,
                Price = model.Out.PartPrice,
                
            };

            var result = await this.context.Parts.AddAsync(part);

            if (result != null)
            {
                return true;
            }

            return false;
        }
    }
}
