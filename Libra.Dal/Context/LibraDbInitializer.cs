using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Dal.Context
{
    public class LibraDbInitializer : CreateDatabaseIfNotExists<LibraContext>
    {
        protected override void Seed(LibraContext context)
        {
            // Seed cities
            var cities = SeedData.GetCities();
            context.Cities.AddRange(cities);

            // Seed weekdays
            var weekDays = SeedData.GetWeekDays();
            context.WeekDays.AddRange(weekDays);

            // Seed user types
            context.UserTypes.AddRange(SeedData.UserTypesSeed);

            // Seed connection types
            context.ConnectionType.AddRange(SeedData.ConnectionTypeSeed);

            // Seed users
            context.Users.AddRange(SeedData.UsersSeed);

            // Seed statuses
            context.Statuses.AddRange(SeedData.statusesSeed);

            // Save changes
            context.SaveChanges();
        }
    }
}
