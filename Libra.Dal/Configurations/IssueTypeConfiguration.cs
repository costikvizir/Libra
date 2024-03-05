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
	public sealed class IssueTypeConfiguration : IEntityTypeConfiguration<IssueType>
	{
		public void Configure(EntityTypeBuilder<IssueType> builder)
		{
			builder.HasKey(e => e.Id);

		    builder.HasMany(e => e.IssueTypes)
				.WithOne(e => e.IssueType)
				.HasForeignKey(e => e.TypeId)
				.OnDelete(DeleteBehavior.Restrict)
			    .IsRequired();

			builder.HasMany(e => e.IssueSubTypes)
				.WithOne(e => e.IssueSubType)
				.HasForeignKey(e => e.SubTypeId)
				.OnDelete(DeleteBehavior.Restrict)
			    .IsRequired();

			builder.HasMany(e => e.IssuesProblems)
				.WithOne(e => e.IssueProblem)
				.HasForeignKey(e => e.ProblemId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
		}
	}
}
