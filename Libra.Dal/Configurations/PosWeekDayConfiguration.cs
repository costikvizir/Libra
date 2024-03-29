using Libra.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Dal.Configurations
{
	public sealed class PosWeekDayConfiguration : IEntityTypeConfiguration<PosWeekDay>
	{
		public void Configure(EntityTypeBuilder<PosWeekDay> builder)
		{
			//builder.HasKey(e => e.Id);

			builder.HasOne(e => e.Pos)
				.WithMany(e => e.PosWeekDays)
				.HasForeignKey(e => e.PosId)
				.IsRequired();

			builder.HasOne(e => e.DayOfWeek)
				.WithMany(e => e.PosWeekDays)
				.HasForeignKey(e => e.WeekDayId)
				.IsRequired();

			builder.Property(p => p.PosId).ValueGeneratedNever();
			builder.Property(p => p.WeekDayId).ValueGeneratedNever();

			builder.HasKey(e => new { e.PosId, e.WeekDayId });
		}
	}
}
