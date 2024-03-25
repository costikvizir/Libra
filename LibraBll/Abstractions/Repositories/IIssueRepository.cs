using LibraBll.DTOs;
using LibraBll.DTOs.Issue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraBll.Abstractions.Repositories
{
	public interface IIssueRepository
	{
		Task<IssueDTO> GetIssueByIdAsync(int id);
		Task<List<IssueDTO>> GetAllIssuesAsync();
		Task<IssueDTO> AddIssue(IssueDTO issue);
		void UpdateIssue(IssueDTO issue);
		void DeleteIssue(int id);
	}
}
