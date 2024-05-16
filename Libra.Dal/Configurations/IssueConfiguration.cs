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
	public class IssueConfiguration : EntityTypeConfiguration<Issue>
	{
        public IssueConfiguration()
        {
            // Define primary key
            this.HasKey(e => e.Id);

            // Define relationship with Logs
            this.HasMany(e => e.Logs)
                .WithRequired(e => e.Issue) // or .WithOptional(e => e.Issue) if Issue is optional in Log
                .HasForeignKey(e => e.IssueId)
                .WillCascadeOnDelete(false); // Specify whether cascading delete is enabled
        }
    }
}
