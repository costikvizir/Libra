using Libra.Dal.Context;
using Libra.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Dal.Configurations
{
	public class WeekDayConfiguration : EntityTypeConfiguration<WeekDay>
	{
        public WeekDayConfiguration()
        {
            // Define primary key
            this.HasKey(e => e.Id);

            // Seed data (if needed)
            // Note: EF6 does not have a built-in seeding mechanism like EF Core, you may need to handle seeding separately
        }
        //TODO: Seed data WeekDay
    }
}
