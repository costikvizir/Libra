using Libra.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraBll.DTOs.Issue
{
	public class IssueDTO
	{
		public int? PosId { get; set; }
		public string Type { get; set; }
		public string SubType { get; set; }
		public string Problem { get; set; }
		public string Priority { get; set; }
		public string Status { get; set; }
		public string Memo { get; set; }
		public string UserCreated { get; set; }
		public string AssignedTo { get; set; }
		public string Description { get; set; }
		public string AssignedDate { get; set; }
		public string CreationDate { get; set; }
		public string ModificationDate { get; set; }
		public string Solution { get; set; }
		public string PosName { get; set; }
		public string UserRole { get; set; }
	}
}
