using Libra.Dal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Dal.Entities
{
	public class IssueType : BaseEntity
	{
		public int IssueLevel { get; set; }
		public int ParrentIssue { get; set; }
		public int IssueNameId { get; set; }
		public DateTime InsertDate { get; set; }
        public IssueName IssueName { get; set; }
		public ICollection<Issue> IssueTypes { get; set; }
		public ICollection<Issue> IssueSubTypes { get; set; }
		public ICollection<Issue> IssuesProblems { get; set; }
	}
}
