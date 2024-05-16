using Libra.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Dal.Configurations
{
	public sealed class LogConfiguration : EntityTypeConfiguration<Log>
	{
		public LogConfiguration() 
		{
            HasKey(e => e.Id);
        }

	}
}
