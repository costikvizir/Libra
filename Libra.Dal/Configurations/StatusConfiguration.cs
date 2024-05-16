using Libra.Dal.Context;
using Libra.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Dal.Configurations
{
	public class StatusConfiguration : EntityTypeConfiguration<Status>
	{
		public StatusConfiguration() 
		{
            this.HasKey(e => e.Id);

            // Define relationship with Issues
            this.HasMany(e => e.Issues)
                .WithRequired(e => e.Status)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);
        }
        //TODO: data seed statuses
    }
}
