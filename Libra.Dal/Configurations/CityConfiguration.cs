using Libra.Dal.Context;
using Libra.Dal.Entities;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Dal.Configurations
{
	public sealed class CityConfiguration : EntityTypeConfiguration<City>
	{
		public CityConfiguration()
		{
            // Define primary key
            this.HasKey(e => e.Id);

            // Define relationship with PosList
            this.HasMany(e => e.PosList)
                .WithRequired(e => e.City)
                .HasForeignKey(e => e.CityId)
                .WillCascadeOnDelete(false);
        }
	}
}
