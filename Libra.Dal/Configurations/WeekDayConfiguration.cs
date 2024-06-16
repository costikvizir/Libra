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
    public class WeekDayConfiguration : EntityTypeConfiguration<WeekDay>
    {
        public WeekDayConfiguration()
        {
            this.HasKey(e => e.Id);
        }
    }
}