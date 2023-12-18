using Libra.Dal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Dal.Entities
{
	public class User : BaseEntity
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public string Telephone { get; set; }
		public int UserTypeId { get; set; }
		public bool IsDeleted { get; set; }
		public UserType UserType { get; set; }
        public ICollection<Log> Logs { get; set; }
		public ICollection<Issue> Issues { get; set; }
	}
}