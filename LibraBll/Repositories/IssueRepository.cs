using Libra.Dal.Entities;
using LibraBll.Abstractions.Repositories;
using LibraBll.Common;
using LibraBll.Common.DataTableModels;
using LibraBll.Common.Extensions;
using LibraBll.DTOs.Issue;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraBll.Repositories
{
    public class IssueRepository : BaseRepository, IIssueRepository
    {
        /// <summary>
        /// TODO: Add Issue issue LEVEL
        /// </summary>
        /// <param name="issuePost"></param>
        /// <returns></returns>
        public async Task<IssueDTO> AddIssue(IssueDTO issuePost)
        {
            IssueType issueType = new IssueType
            {
                Name = issuePost.Type,
                IssueLevel = 1,
                ParrentIssue = 0,
                InsertDate = DateTime.Now
            };

            await Context.IssueTypes.AddAsync(issueType);
            await Context.SaveChangesAsync();

            int typeId = Context.IssueTypes.Where(t => t.Name == issuePost.Type).Select(t => t.Id).FirstOrDefault();
            int subTypeId = Context.IssueTypes.Where(t => t.Name == issuePost.SubType).Select(t => t.Id).FirstOrDefault();
            int problemId = Context.IssueTypes.Where(t => t.Name == issuePost.Problem).Select(t => t.Id).FirstOrDefault();
            int statusId = Context.Statuses.Where(s => s.IssueStatus == issuePost.Status).Select(s => s.Id).FirstOrDefault();
            int userCreatedId = Context.Users.Where(u => u.Name == issuePost.UserCreated).Select(u => u.Id).FirstOrDefault();
            int assignedId = Context.UserTypes.Where(u => u.Role == issuePost.AssignedTo).Select(u => u.Id).FirstOrDefault();

            Issue issue = new Issue();

            issue.PosId = issuePost.PosId;
            issue.TypeId = typeId;
            issue.SubTypeId = subTypeId;
            issue.ProblemId = problemId;
            issue.Priority = issuePost.Priority;
            issue.StatusId = statusId;
            issue.Memo = issuePost.Memo;
            issue.UserCreatedId = userCreatedId;
            issue.AssignedId = assignedId;
            issue.Description = issuePost.Description;
            //issue.AssignedDate = DateTime.Parse(issuePost.AssignedDate);
            issue.AssignedDate = DateTime.Now;
            issue.CreationDate = DateTime.Now;
            //issue.ModificationDate = DateTime.Parse(issuePost.ModificationDate);
            issue.Solution = issuePost.Solution;

            Context.Issues.Add(issue);
            await Context.SaveChangesAsync();

            return issuePost;
        }

        public void DeleteIssue(int id)
        {
            //var issue =  Context.Issues.Find(id);
            try
            {
                var issue = Context.Issues.Where(i => i.Id == id).FirstOrDefault();

                issue.IsDeleted = true;

                //Context.Entry(issue).State = EntityState.Modified;
                Context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetIssueCount()
        {
            return Context.Issues.Count();
        }

        public async Task<List<IssueDTO>> GetAllIssuesAsync(DataTablesParameters parameters, CancellationToken cancellationToken)
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
                        Id = i.Id,
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
                        AssignedDate = i.AssignedDate.ToString("dd/MM/yyyy"),
                        CreationDate = i.CreationDate.ToString("dd/MM/yyyy"),
                        ModificationDate = i.ModificationDate.ToString(),
                        Solution = i.Solution,
                        PosName = i.Pos.Name,
                        UserRole = i.UserType.Role
                    })
                    .Search(parameters)
                    .OrderBy(parameters)
                    .Page(parameters)
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
            var issueDTO = new IssueDTO();
            //var issueType = await Context.IssueTypes.FirstOrDefaultAsync(x => x.IssueTypes == issue.IssueType);
            string type = Context.IssueTypes.Where(t => t.Id == issue.TypeId).Select(t => t.Name).FirstOrDefault();
            string subType = Context.IssueTypes.Where(t => t.Id == issue.SubTypeId).Select(t => t.Name).FirstOrDefault();
            string problem = Context.IssueTypes.Where(t => t.Id == issue.ProblemId).Select(t => t.Name).FirstOrDefault();
            string status = Context.Statuses.Where(s => s.Id == issue.StatusId).Select(s => s.IssueStatus).FirstOrDefault();
            string userCreated = Context.Users.Where(u => u.Id == issue.UserCreatedId).Select(u => u.Name).FirstOrDefault();
            string assignedTo = Context.UserTypes.Where(u => u.Id == issue.AssignedId).Select(u => u.Role).FirstOrDefault();
            string posName = Context.Pos.Where(p => p.Id == issue.PosId).Select(p => p.Name).FirstOrDefault();
            //string userRole = Context.UserTypes.Where(x => x.Id == issue.UserCreatedId).Select(x => x.Role).FirstOrDefault();

            issueDTO.Id = issue.Id;
            issueDTO.PosId = issue.PosId;
            issueDTO.Type = type;
            issueDTO.SubType = subType;
            issueDTO.Problem = problem;
            issueDTO.Status = status;
            //issueDTO.Type = issue.IssueType?.Name;
            //issueDTO.SubType = issue.IssueSubType?.Name;
            //issueDTO.Problem = issue.IssueProblem?.Name;
            issueDTO.Priority = issue.Priority;
            //issueDTO.Status = issue.Status.IssueStatus;
            issueDTO.Memo = issue.Memo;
            //issueDTO.UserCreated = issue.User.Name;
            //issueDTO.AssignedTo = issue.UserType.Role;
            issueDTO.UserCreated = userCreated;
            issueDTO.AssignedTo = assignedTo;
            issueDTO.Description = issue.Description;
            issueDTO.AssignedDate = issue.AssignedDate.ToString();
            issueDTO.CreationDate = issue.CreationDate.ToString();
            issueDTO.ModificationDate = issue.ModificationDate.ToString();
            issueDTO.Solution = issue.Solution;
            //issueDTO.PosName = issue.Pos.Name;
            //issueDTO.UserRole = issue.UserType.Role;

            return issueDTO;
            //return new IssueDTO
            //{
            //	Id = issue.Id,
            //	PosId = issue.PosId,
            //	Type = issue.IssueType.Name,
            //	SubType = issue.IssueSubType.Name,
            //	Problem = issue.IssueProblem.Name,
            //	Priority = issue.Priority,
            //	Status = issue.Status.IssueStatus,
            //	Memo = issue.Memo,
            //	UserCreated = issue.User.Name,
            //	AssignedTo = issue.UserType.Role,
            //	Description = issue.Description,
            //	AssignedDate = issue.AssignedDate.ToString(),
            //	CreationDate = issue.CreationDate.ToString(),
            //	ModificationDate = issue.ModificationDate.ToString(),
            //	Solution = issue.Solution,
            //	PosName = issue.Pos.Name,
            //	UserRole = issue.UserType.Role
            //};
        }

        public async Task<List<IssueDTO>> GetIssuesByPosIdAsync(int posId)
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
                    .Where(i => i.PosId == posId)
                    .Select(i => new IssueDTO
                    {
                        Id = i.Id,
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
                        AssignedDate = i.AssignedDate.ToString("dd/MM/yyyy"),
                        CreationDate = i.CreationDate.ToString("dd/MM/yyyy"),
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