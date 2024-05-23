
using Libra.Dal.Entities;
using System.Data.Entity.ModelConfiguration;


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
    }
}
