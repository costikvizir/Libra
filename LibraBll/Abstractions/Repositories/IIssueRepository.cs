using LibraBll.Common.DataTableModels;
using LibraBll.DTOs.ComplexObjects;
using LibraBll.DTOs.Dropdown;
using LibraBll.DTOs.Issue;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LibraBll.Abstractions.Repositories
{
    public interface IIssueRepository
    {
        Task<IssueGetDTO> GetIssueByIdAsync(int id);

        Task<List<IssueGetDTO>> GetAllIssuesAsync(DataTablesParameters parameters, CancellationToken cancellationToken);

        Task<List<IssueGetDTO>> GetIssuesByPosIdAsync(int posId);

        Task<IssuePostDTO> AddIssue(IssuePostDTO issue);

        //void UpdateIssue(IssueDTO issue);

        void DeleteIssue(int id);

        Task<int> GetIssueCount();

        Task<List<StatusDTO>> GetStatusList();

        Task<List<PriorityDTO>> GetPriorityList();

        Task<List<IssueNameDTO>> GetIssueNameList(int? id);

        Task<StatusGroupCount> GetStatusGroupCount();
    }
}