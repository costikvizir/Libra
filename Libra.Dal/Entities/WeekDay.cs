using Libra.Dal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Dal.Entities
{
	public class WeekDay : BaseEntity
	{
        public string Day { get; set; }
		public ICollection<PosWeekDay> PosWeekDays { get; set; }
    }
}
