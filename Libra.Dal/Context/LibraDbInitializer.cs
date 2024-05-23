using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Dal.Context
{
    public class LibraDbInitializer : DbMigrationsConfiguration<LibraContext>
    {

        public LibraDbInitializer()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
        }
        protected override void Seed(LibraContext context)
        {
            // Seed cities
            var cities = SeedData.GetCities();
            var weekDays = SeedData.GetWeekDays();

            if(!context.Cities.Any())
            {
                context.Cities.AddRange(cities);
            }

            // Seed weekdays
            if(!context.WeekDays.Any())
            {
                context.WeekDays.AddRange(weekDays);
            }

            // Seed user types
            if (!context.UserTypes.Any())
            {
                context.UserTypes.AddRange(SeedData.UserTypesSeed);
            }
            // Seed connection types

            if(!context.ConnectionType.Any())
            {
                context.ConnectionType.AddRange(SeedData.ConnectionTypeSeed);
            }

            // Seed users
            if (!context.Users.Any())
            {
                context.Users.AddRange(SeedData.UsersSeed);
            }
            // Seed statuses
            if (!context.Statuses.Any())
            {
                context.Statuses.AddRange(SeedData.statusesSeed);
            }
            // Seed priorities
            if (!context.Priorities.Any())
            {
                context.Priorities.AddRange(SeedData.PrioritiesSeed);
            }
            // Seed issuenames
            if (!context.IssueNames.Any())
            {
                context.IssueNames.AddRange(SeedData.IssueNamesSeed);
            }

            // Save changes
            context.SaveChanges();
        }
        //public Configuration()
        //{
        //    AutomaticMigrationsEnabled = true;
        //    //AutomaticMigrationDataLossAllowed = true;
        //}

        //protected override void Seed(LibraContext context)
        //{
        //}
    }
}

