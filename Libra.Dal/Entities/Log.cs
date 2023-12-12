using Libra.Dal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Dal.Entities
{
	public class Log : BaseEntity
	{
		public int IssueId { get; set; }
		public int UserId { get; set; }
		public string Action { get; set; }
		public string Notes { get; set; }
		public DateTime InsertDate { get; set; }
        public Issue Issue { get; set; }
        public User User { get; set; }
    }
}
