using Libra.Dal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Dal.Entities
{
	public class City : BaseEntity
	{
		public string CityName { get; set; }
		public ICollection<Pos> PosList { get; set; }
	}
}
