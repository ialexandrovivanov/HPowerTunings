using HPowerTunings.Data;
using HPowerTunings.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HPowerTunings.Web.Core
{
    public class DayCreator : IDayCreator
    {
        private ApplicationDbContext context;

        public DayCreator(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task CreateDay()
        {
            var currentDay = this.context.Days.FirstOrDefault(d => d.DayDateTime.Value.Date == DateTime.Now.Date);
            if (currentDay == null)
            {
                this.context
                    .Days
                    .Add(new Day
                    {
                        DayDateTime = DateTime.Now,
                        Description = "Created by dayCreator",
                        DaylyExpenses = 0,
                    });

                context.SaveChangesAsync().GetAwaiter().GetResult();
            }
            while (true)
            {
                var secondsToMidnight = (int)(DateTime.Today.AddDays(1) - DateTime.Now).TotalSeconds;

                while (secondsToMidnight-- > -2)
                {
                    await Task.Delay(1000);
                }

                if (!this.context.Days.Any(d => d.DayDateTime == DateTime.Now.Date ))
                {
                    await this.context.Days.AddAsync(new Day() { DayDateTime = DateTime.Now.Date, DaylyExpenses = 0 });

                    await this.context.SaveChangesAsync();
                }
            }
        }
    }
}
