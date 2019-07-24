using HPowerTunings.Data;
using HPowerTunings.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace HPowerTunings.Web.Core
{
    public class AddCarModels
    {
        private ApplicationDbContext context;
        public AddCarModels(ApplicationDbContext context)
        {
            this.context = context;
        }

        private void AddModels()
        {
            var brand = context.CarBrands.FirstOrDefault(b => b.Id == "bmv");
            var models = new List<string>() { "E28", "E30", "E36", "E38", "Z3", "E39", "E46", "E52", "E53", "E65" };
            foreach (var model in models)
            {
                brand.Models.Add(new CarModel() { Name = model, CarBrandId = "bmv" });
            }
            context.SaveChanges();
        }
    }
}
