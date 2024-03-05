using Libra.Dal.Context;
using Libra.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Dal.Configurations
{
	public sealed class CityConfiguration : IEntityTypeConfiguration<City>
	{
		public void Configure(EntityTypeBuilder<City> builder)
		{
			builder.HasKey(e => e.Id);

			builder.HasMany(e => e.PosList)
				.WithOne(e => e.City)
				.HasForeignKey(e => e.CityId)
				.IsRequired();

			builder.HasData(SeedData.GetCities());
		}
	}
}
