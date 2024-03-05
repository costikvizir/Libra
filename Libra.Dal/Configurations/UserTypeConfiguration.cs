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
	public sealed class UserTypeConfiguration : IEntityTypeConfiguration<UserType>
	{
		public void Configure(EntityTypeBuilder<UserType> builder)
		{
			builder.HasKey(e => e.Id);

	        builder.HasMany(e => e.Issues)
				.WithOne(e => e.UserType)
				.HasForeignKey(e => e.AssignedId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();

			builder.HasMany(e => e.Users)
				.WithOne(e => e.UserType)
				.HasForeignKey(e => e.UserTypeId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			builder.HasData(SeedData.UserTypesSeed);
		}
	}
}
