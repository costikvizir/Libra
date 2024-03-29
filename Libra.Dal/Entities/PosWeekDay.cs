using Libra.Dal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Dal.Entities
{
	public class PosWeekDay //: BaseEntity
	{
		public int PosId { get; set; }
		public int WeekDayId { get; set; }
		public Pos Pos { get; set; }
		public WeekDay DayOfWeek { get; set; }
	}
}
