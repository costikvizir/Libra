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
	public sealed class PosConfiguration : EntityTypeConfiguration<Pos>
	{
        public PosConfiguration()
        {
            this.HasKey(e => e.Id);

            // Define relationship with Issues
            this.HasMany(e => e.Issues)
                .WithRequired(e => e.Pos)
                .HasForeignKey(e => e.PosId)
                .WillCascadeOnDelete(false);
        }
    }
}
