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
            int typeId = Context.IssueTypes.Where(t => t.Name == issuePost.Type).Select(t => t.Id).FirstOrDefault();
            int subTypeId = Context.IssueTypes.Where(t => t.Name == issuePost.SubType).Select(t => t.Id).FirstOrDefault();
            int problemId = Context.IssueTypes.Where(t => t.Name == issuePost.Problem).Select(t => t.Id).FirstOrDefault();
            int statusId = Context.Statuses.Where(s => s.IssueStatus == issuePost.Status).Select(s => s.Id).FirstOrDefault();
            int userCreatedId = Context.Users.Where(u => u.Name == issuePost.UserCreated).Select(u => u.Id).FirstOrDefault();
            int assignedId = Context.UserTypes.Where(u => u.Role == issuePost.AssignedTo).Select(u => u.Id).FirstOrDefault();
            
            Issue issue = new Issue
            {
                PosId = issuePost.PosId,
                TypeId = typeId,
                SubTypeId = subTypeId,
                ProblemId = problemId,
                Priority = issuePost.Priority,
                StatusId = statusId,
                Memo = issuePost.Memo,
                UserCreatedId = userCreatedId,
                AssignedId = assignedId,
                Description = issuePost.Description,
                AssignedDate = DateTime.Parse(issuePost.AssignedDate),
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Parse(issuePost.ModificationDate),
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
                        Type = i.IssueType.Name,
                        SubType = i.IssueSubType.Name,
                        Problem = i.IssueProblem.Name,
                        Priority = i.Priority,
                        Status = i.Status.IssueStatus,
                        Memo = i.Memo,
                        UserCreated = i.User.Name,
                        AssignedTo = i.UserType.Role,
                        Description = i.Description,
                        AssignedDate = i.AssignedDate.ToString(),
                        CreationDate = i.CreationDate.ToString(),
                        ModificationDate = i.ModificationDate.ToString(),
                        Solution = i.Solution,
                        PosName = i.Pos.Name,
                        UserRole = i.UserType.Role
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
				Type = issue.IssueType.Name,
				SubType = issue.IssueSubType.Name,
				Problem = issue.IssueProblem.Name,
				Priority = issue.Priority,
				Status = issue.Status.IssueStatus,
				Memo = issue.Memo,
				UserCreated = issue.User.Name,
				AssignedTo = issue.UserType.Role,
				Description = issue.Description,
				AssignedDate = issue.AssignedDate.ToString(),
				CreationDate = issue.CreationDate.ToString(),
				ModificationDate = issue.ModificationDate.ToString(),
				Solution = issue.Solution,
				PosName = issue.Pos.Name,
				UserRole = issue.UserType.Role
			};
        }

        public async void UpdateIssue(IssueDTO issuePost)
        {
			int typeId = Context.IssueTypes.Where(t => t.Name == issuePost.Type).Select(t => t.Id).FirstOrDefault();
			int subTypeId = Context.IssueTypes.Where(t => t.Name == issuePost.SubType).Select(t => t.Id).FirstOrDefault();
			int problemId = Context.IssueTypes.Where(t => t.Name == issuePost.Problem).Select(t => t.Id).FirstOrDefault();
			int statusId = Context.Statuses.Where(s => s.IssueStatus == issuePost.Status).Select(s => s.Id).FirstOrDefault();
			int userCreatedId = Context.Users.Where(u => u.Name == issuePost.UserCreated).Select(u => u.Id).FirstOrDefault();
			int assignedId = Context.UserTypes.Where(u => u.Role == issuePost.AssignedTo).Select(u => u.Id).FirstOrDefault();

			Issue issue = new Issue
			{
				PosId = issuePost.PosId,
				TypeId = typeId,
				SubTypeId = subTypeId,
				ProblemId = problemId,
				Priority = issuePost.Priority,
				StatusId = statusId,
				Memo = issuePost.Memo,
				UserCreatedId = userCreatedId,
				AssignedId = assignedId,
				Description = issuePost.Description,
				AssignedDate = DateTime.Parse(issuePost.AssignedDate),
				CreationDate = DateTime.Now,
				ModificationDate = DateTime.Parse(issuePost.ModificationDate),
				Solution = issuePost.Solution
			};

			Context.Issues.Add(issue);
            await Context.SaveChangesAsync();
        }
    }
}