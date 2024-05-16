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
	public class UserTypeConfiguration : EntityTypeConfiguration<UserType>
	{
        public UserTypeConfiguration()
        {
            // Define primary key
            this.HasKey(e => e.Id);

            // Define relationship with Issues
            this.HasMany(e => e.Issues)
                .WithRequired(e => e.UserType)
                .HasForeignKey(e => e.AssignedId)
                .WillCascadeOnDelete(false); // Specify whether cascading delete is enabled

            // Define relationship with Users
            this.HasMany(e => e.Users)
                .WithRequired(e => e.UserType)
                .HasForeignKey(e => e.UserTypeId)
                .WillCascadeOnDelete(true); // Specify whether cascading delete is enabled

            // Seed data (if needed)
            // Note: EF6 does not have a built-in seeding mechanism like EF Core, you may need to handle seeding separately
        }

        //TODO: Seed data usertype
    }
}
