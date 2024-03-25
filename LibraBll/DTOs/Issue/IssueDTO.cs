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
		public int? TypeId { get; set; }
		public int? SubTypeId { get; set; }
		public int? ProblemId { get; set; }
		public string Priority { get; set; }
		public int? StatusId { get; set; }
		public string Memo { get; set; }
		public int? UserCreatedId { get; set; }
		public int? AssignedId { get; set; }
		public string Description { get; set; }
		public DateTime AssignedDate { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime ModificationDate { get; set; }
		public string Solution { get; set; }
		public Pos Pos { get; set; }
		public Status Status { get; set; }
		public User User { get; set; }
		public UserType UserType { get; set; }
		public IssueType IssueType { get; set; }
		public IssueType IssueSubType { get; set; }
		public IssueType IssueProblem { get; set; }
	}
}
