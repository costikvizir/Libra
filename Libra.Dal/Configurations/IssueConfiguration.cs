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
	public sealed class IssueConfiguration : IEntityTypeConfiguration<Issue>
	{
		public void Configure(EntityTypeBuilder<Issue> builder)
		{
			builder.HasKey(e => e.Id);

		    builder.HasMany(e => e.Logs)
			   .WithOne(e => e.Issue)
			   .HasForeignKey(e => e.IssueId)
			   .IsRequired();
		}
	}
}
