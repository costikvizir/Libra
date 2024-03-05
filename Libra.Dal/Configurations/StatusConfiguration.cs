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
	public sealed class StatusConfiguration : IEntityTypeConfiguration<Status>
	{
		public void Configure(EntityTypeBuilder<Status> builder)
		{
			builder.HasKey(e => e.Id);

			   builder.HasMany(e => e.Issues)
			   .WithOne(e => e.Status)
			   .HasForeignKey(e => e.StatusId)
			   .IsRequired();
		}
	}
}
