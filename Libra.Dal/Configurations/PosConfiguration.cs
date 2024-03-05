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
	public sealed class PosConfiguration : IEntityTypeConfiguration<Pos>
	{
		public void Configure(EntityTypeBuilder<Pos> builder)
		{
			builder.HasKey(e => e.Id);

		    builder.HasMany(e => e.Issues)
			   .WithOne(e => e.Pos)
			   .HasForeignKey(e => e.PosId)
			   .IsRequired();
		}
	}
}
