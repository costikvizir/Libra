using Libra.Dal.Context;
using Libra.Dal.Entities;

using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Dal.Configurations
{
    public sealed class IssueTypeConfiguration : EntityTypeConfiguration<IssueType>
    {
        public IssueTypeConfiguration()
        {
            // Define primary key
            this.HasKey(e => e.Id);

            // Define relationship with IssueTypes
            this.HasMany(e => e.IssueTypes)
                .WithRequired(e => e.IssueType)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false); // Specify whether cascading delete is enabled

            // Define relationship with IssueSubTypes
            this.HasMany(e => e.IssueSubTypes)
                .WithRequired(e => e.IssueSubType)
                .HasForeignKey(e => e.SubTypeId)
                .WillCascadeOnDelete(false); // Specify whether cascading delete is enabled

            // Define relationship with IssuesProblems
            this.HasMany(e => e.IssuesProblems)
                .WithRequired(e => e.IssueProblem)
                .HasForeignKey(e => e.ProblemId)
                .WillCascadeOnDelete(false); // Specify whether cascading delete is enabled
        }
    }
}
