using Libra.Dal.Context;
using Libra.Dal.Entities;
using System.Data.Entity.ModelConfiguration;


namespace Libra.Dal.Configurations
{
	public class UserConfiguration : EntityTypeConfiguration<User>
	{
        public UserConfiguration()
        {
            // Define primary key
            this.HasKey(x => x.Id);

            // Configure property Name
            this.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Define relationship with Issues
            this.HasMany(e => e.Issues)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false); // Specify whether cascading delete is enabled

            // Define relationship with Logs
            this.HasMany(e => e.Logs)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false); // Specify whether cascading delete is enabled

            // Seed data (if needed)
            // Note: EF6 does not have a built-in seeding mechanism like EF Core, you may need to handle seeding separately
        }
        //TODO: Seed data User
    }
}
