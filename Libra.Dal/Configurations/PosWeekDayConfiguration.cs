using Libra.Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Dal.Configurations
{
	public class PosWeekDayConfiguration : EntityTypeConfiguration<PosWeekDay>
	{
        public PosWeekDayConfiguration()
        {
            // Define composite primary key
            this.HasKey(e => new { e.PosId, e.WeekDayId });

            // Define relationship with Pos
            this.HasRequired(e => e.Pos)
                .WithMany(e => e.PosWeekDays)
                .HasForeignKey(e => e.PosId);

            // Define relationship with DayOfWeek
            this.HasRequired(e => e.DayOfWeek)
                .WithMany(e => e.PosWeekDays)
                .HasForeignKey(e => e.WeekDayId);

            // Configure properties PosId and WeekDayId
            this.Property(p => p.PosId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(p => p.WeekDayId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
