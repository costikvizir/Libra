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
	public sealed class ConnectionTypeConfiguration : IEntityTypeConfiguration<ConnectionType>
	{
		public void Configure(EntityTypeBuilder<ConnectionType> builder)
		{
			builder.HasKey(e => e.Id);

			builder.HasMany(e => e.PosList)
			   .WithOne(e => e.ConnectionType)
			   .HasForeignKey(e => e.ConnectionTypeId)
			   .IsRequired();
		}
	}
}
