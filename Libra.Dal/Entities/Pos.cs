using Libra.Dal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Dal.Entities
{
	public class Pos : BaseEntity
	{
		public string Name { get; set; }
		public string Telephone { get; set; }
		public string Cellphone { get; set; }
		public string Address { get; set; }
		public int CityId { get; set; }
		public string Model { get; set; }
		public string Brand { get; set; }
		public int ConnectionTypeId { get; set; }
		public TimeSpan MorningOpening { get; set; }
		public TimeSpan MorningClosing { get; set; }
		public TimeSpan AfternoonOpening { get; set; }
		public TimeSpan AfternoonClosing { get; set; }
		public string DaysClosed { get; set; }
		public DateTime InsertDate { get; set; }
        public City City { get; set; }
        public ConnectionType ConnectionType { get; set; }
        public ICollection<Issue> Issues { get; set; }
	}
}
