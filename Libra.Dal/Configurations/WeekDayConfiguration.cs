using Libra.Dal.Context;
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
	public sealed class WeekDayConfiguration : IEntityTypeConfiguration<WeekDay>
	{
		public void Configure(EntityTypeBuilder<WeekDay> builder)
		{
			builder.HasKey(e => e.Id);

			builder.HasData(SeedData.GetWeekDays());
		}
	}
}
