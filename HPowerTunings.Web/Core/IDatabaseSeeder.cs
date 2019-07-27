using HPowerTunings.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPowerTunings.Web.Core
{
    public interface IDatabaseSeeder
    {
        Task SeedDatabase();
        Task InsertRecords(CarBrand carBrand, List<string> models);
    }
}
