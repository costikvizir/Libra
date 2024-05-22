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
            this.HasKey(e => e.Id);

            this.HasMany(e => e.Issues)
                .WithRequired(e => e.UserType)
                .HasForeignKey(e => e.AssignedId)
                .WillCascadeOnDelete(false); 

            this.HasMany(e => e.Users)
                .WithRequired(e => e.UserType)
                .HasForeignKey(e => e.UserTypeId)
                .WillCascadeOnDelete(true); 
        }
    }
}
