using Libra.Dal.Entities;
using LibraBll.Abstractions.Repositories;
using LibraBll.Common;
using LibraBll.DTOs.Issue;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraBll.Repositories
{
    public class IssueRepository : BaseRepository, IIssueRepository
    {
        public async Task<IssueDTO> AddIssue(IssueDTO issuePost)
        {
            Issue issue = new Issue
            {
                PosId = issuePost.PosId,
                TypeId = issuePost.TypeId,
                SubTypeId = issuePost.SubTypeId,
                ProblemId = issuePost.ProblemId,
                Priority = issuePost.Priority,
                StatusId = issuePost.StatusId,
                Memo = issuePost.Memo,
                UserCreatedId = issuePost.UserCreatedId,
                AssignedId = issuePost.AssignedId,
                Description = issuePost.Description,
                AssignedDate = issuePost.AssignedDate,
                CreationDate = issuePost.CreationDate,
                ModificationDate = issuePost.ModificationDate,
                Solution = issuePost.Solution
            };

            Context.Issues.Add(issue);
            await Context.SaveChangesAsync();

            return issuePost;
        }

        public async void DeleteIssue(int id)
        {
            var issue = await Context.Issues.FindAsync(id);
            Context.Issues.Remove(issue);
            await Context.SaveChangesAsync();
        }

        public async Task<List<IssueDTO>> GetAllIssuesAsync()
        {
            List<IssueDTO> issueList = null;
            try
            {
                issueList = await Context.Issues
                    .Include(i => i.Pos)
                    .Include(i => i.Status)
                    .Include(i => i.User)
                    .Include(i => i.UserType)
                    .Include(i => i.IssueType)
                    .Include(i => i.IssueSubType)
                    .Include(i => i.IssueProblem)
                    .Select(i => new IssueDTO
                    {
                        PosId = i.PosId,
                        TypeId = i.TypeId,
                        SubTypeId = i.SubTypeId,
                        ProblemId = i.ProblemId,
                        Priority = i.Priority,
                        StatusId = i.StatusId,
                        Memo = i.Memo,
                        UserCreatedId = i.UserCreatedId,
                        AssignedId = i.AssignedId,
                        Description = i.Description,
                        AssignedDate = i.AssignedDate,
                        CreationDate = i.CreationDate,
                        ModificationDate = i.ModificationDate,
                        Solution = i.Solution,
                        Pos = i.Pos,
                        Status = i.Status,
                        User = i.User,
                        UserType = i.User.UserType,
                        IssueType = i.IssueType,
                        IssueSubType = i.IssueSubType,
                        IssueProblem = i.IssueProblem
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return issueList;
        }

        public async Task<IssueDTO> GetIssueByIdAsync(int id)
        {
            var issue = await Context.Issues.FindAsync(id);

            return new IssueDTO
            {
                PosId = issue.PosId,
                TypeId = issue.TypeId,
                SubTypeId = issue.SubTypeId,
                ProblemId = issue.ProblemId,
                Priority = issue.Priority,
                StatusId = issue.StatusId,
                Memo = issue.Memo,
                UserCreatedId = issue.UserCreatedId,
                AssignedId = issue.AssignedId,
                Description = issue.Description,
                AssignedDate = issue.AssignedDate,
                CreationDate = issue.CreationDate,
                ModificationDate = issue.ModificationDate,
                Solution = issue.Solution,
                Pos = issue.Pos,
                Status = issue.Status,
                User = issue.User,
                UserType = issue.UserType,
                IssueType = issue.IssueType,
                IssueSubType = issue.IssueSubType,
                IssueProblem = issue.IssueProblem
            };
        }

        public async void UpdateIssue(IssueDTO issuePost)
        {
            Issue issue = new Issue
            {
                PosId = issuePost.PosId,
                TypeId = issuePost.TypeId,
                SubTypeId = issuePost.SubTypeId,
                ProblemId = issuePost.ProblemId,
                Priority = issuePost.Priority,
                StatusId = issuePost.StatusId,
                Memo = issuePost.Memo,
                UserCreatedId = issuePost.UserCreatedId,
                AssignedId = issuePost.AssignedId,
                Description = issuePost.Description,
                AssignedDate = issuePost.AssignedDate,
                CreationDate = issuePost.CreationDate,
                ModificationDate = issuePost.ModificationDate,
                Solution = issuePost.Solution
            };

            Context.Issues.Add(issue);
            await Context.SaveChangesAsync();
        }
    }
}