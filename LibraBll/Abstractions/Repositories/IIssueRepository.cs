using LibraBll.Common.DataTableModels;
using LibraBll.DTOs;
using LibraBll.DTOs.ComplexObjects;
using LibraBll.DTOs.Dropdown;
using LibraBll.DTOs.Issue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraBll.Abstractions.Repositories
{
    public interface IIssueRepository
    {
        Task<IssueDTO> GetIssueByIdAsync(int id);

        Task<List<IssueDTO>> GetAllIssuesAsync(DataTablesParameters parameters, CancellationToken cancellationToken);

        Task<List<IssueDTO>> GetIssuesByPosIdAsync(int posId);

        Task<IssueDTO> AddIssue(IssueDTO issue);

        void UpdateIssue(IssueDTO issue);

        void DeleteIssue(int id);

        Task<int> GetIssueCount();

        Task<List<StatusDTO>> GetStatusList();

        Task<List<PriorityDTO>> GetPriorityList();

        Task<List<IssueNameDTO>> GetIssueNameList();

        Task<StatusGroupCount> GetStatusGroupCount();
    }
}