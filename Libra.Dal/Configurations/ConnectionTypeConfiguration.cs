using Libra.Dal.Context;
using Libra.Dal.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Dal.Configurations
{
	public sealed class ConnectionTypeConfiguration : EntityTypeConfiguration<ConnectionType>
	{
        public ConnectionTypeConfiguration()
        {
            // Define primary key
            this.HasKey(e => e.Id);

            // Define relationship with PosList
            this.HasMany(e => e.PosList)
                .WithRequired(e => e.ConnectionType)
                .HasForeignKey(e => e.ConnectionTypeId)
                .WillCascadeOnDelete(false); // Specify whether cascading delete is enabled
        }
    }
}
