using Libra.Dal.Context;
using Libra.Dal.Entities;
using LibraBll.Abstractions.Repositories;
using LibraBll.Common;
using LibraBll.Common.DataTableModels;
using LibraBll.Common.Extensions;
using LibraBll.DTOs.ComplexObjects;
using LibraBll.DTOs.Dropdown;
using LibraBll.DTOs.Issue;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraBll.Repositories
{
    public class IssueRepository : BaseRepository, IIssueRepository
    {
        //public IssueRepository(LibraContext context) : base(context)
        //{
        //}

        private List<Issue> _issues;

        public async Task<IssuePostDTO> AddIssue(IssuePostDTO issuePost)
        {
            try
            {
                List<IssueType> issueTypes = new List<IssueType>
            {
                new IssueType
                {
                    IssueNameId = issuePost.Type,
                    IssueLevel = await Context.IssueNames.Where(i => i.Id == issuePost.Type).Select(x => x.IssueRank).FirstOrDefaultAsync(),
                    ParrentIssue = 0,
                    InsertDate = DateTime.Now
                },
                new IssueType
                {
                    IssueNameId = issuePost.SubType,
                    IssueLevel = await Context.IssueNames.Where(i => i.Id == issuePost.SubType).Select(x => x.IssueRank).FirstOrDefaultAsync(),
                    ParrentIssue = issuePost.Type,
                    InsertDate = DateTime.Now
                },
                new IssueType
                {
                    IssueNameId = issuePost.Problem,
                    IssueLevel = await Context.IssueNames.Where(i => i.Id == issuePost.Problem).Select(x => x.IssueRank).FirstOrDefaultAsync(),
                    ParrentIssue = issuePost.SubType,
                    InsertDate = DateTime.Now
                }
            };

                // Add all IssueType entities to the context
                Context.IssueTypes.AddRange(issueTypes);

                await Context.SaveChangesAsync();

                int issueTypeId = issueTypes[0].Id;
                int issueSubtypeId = issueTypes[1].Id;
                int problemId = issueTypes[2].Id;

                int userCreatedId = await Context.Users.Where(u => u.Name == issuePost.UserCreated).Select(u => u.Id).FirstOrDefaultAsync();

                // Create the Issue entity
                Issue issue = new Issue
                {
                    PosId = issuePost.PosId,
                    TypeId = issueTypeId,
                    SubTypeId = issueSubtypeId,
                    ProblemId = problemId,
                    PriorityId = issuePost.Priority,
                    StatusId = issuePost.StatusId,
                    Memo = issuePost.Memo,
                    UserCreatedId = userCreatedId,
                    AssignedId = issuePost.AssignedTo,
                    Description = issuePost.Description,
                    AssignedDate = DateTime.Now,
                    CreationDate = DateTime.Now,
                    Solution = issuePost.Solution
                };

                Context.Issues.Add(issue);

                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }

            return issuePost;
        }

        public void DeleteIssue(int id)
        {
            var issue = Context.Issues.Find(id);

            if (issue != null)
                issue.IsDeleted = true;

            Context.SaveChanges();
        }

        public async Task<int> GetIssueCount()
        {
            return await Context.Issues.Where(i => i.IsDeleted == false).CountAsync();
        }

        public async Task<List<IssueGetDTO>> GetAllIssuesAsync(DataTablesParameters parameters, CancellationToken cancellationToken)
        {
            var rawIssueList = await Context.Issues
                    .Include(i => i.Pos)
                    .Include(i => i.Status)
                    .Include(i => i.User)
                    .Include(i => i.Priority)
                    .Include(i => i.UserType)
                    .Include(i => i.IssueType)
                    .Include(i => i.IssueSubType)
                    .Include(i => i.IssueProblem)
                    .Where(i => i.IsDeleted == false)
                    .ToListAsync();

            List<IssueGetDTO> issueList = null;
            try
            {
                issueList = rawIssueList
                    .Select(i => new IssueGetDTO
                    {
                        Id = i.Id,
                        Priority = i.Priority.IssuePriority,
                        Type = Context.IssueNames.Where(n => n.Id == i.TypeId).Select(n => n.Name).FirstOrDefault(),
                        SubType = Context.IssueNames.Where(n => n.Id == i.SubTypeId).Select(n => n.Name).FirstOrDefault(),
                        Problem = Context.IssueNames.Where(n => n.Id == i.ProblemId).Select(n => n.Name).FirstOrDefault(),
                        Memo = i.Memo,
                        Status = i.Status.IssueStatus,
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
                    .AsQueryable()
                    .Search(parameters)
                    .OrderBy(parameters)
                    .Page(parameters)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return issueList;
        }

        public async Task<IssueGetDTO> GetIssueByIdAsync(int id)
        {
            var issue = await Context.Issues.FindAsync(id);
            var issueDTO = new IssueGetDTO();
            //var issueType = await Context.IssueTypes.FirstOrDefaultAsync(x => x.IssueTypes == issue.IssueType);
            int type = Context.IssueTypes.Where(t => t.Id == issue.TypeId).Select(t => t.IssueNameId).FirstOrDefault();
            int subType = Context.IssueTypes.Where(t => t.Id == issue.SubTypeId).Select(t => t.IssueNameId).FirstOrDefault();
            int problem = Context.IssueTypes.Where(t => t.Id == issue.ProblemId).Select(t => t.IssueNameId).FirstOrDefault();
            string priority = Context.Priorities.Where(p => p.Id == issue.PriorityId).Select(p => p.IssuePriority).FirstOrDefault();
            string status = Context.Statuses.Where(s => s.Id == issue.StatusId).Select(s => s.IssueStatus).FirstOrDefault();
            string userCreated = Context.Users.Where(u => u.Id == issue.UserCreatedId).Select(u => u.Name).FirstOrDefault();
            string assignedTo = Context.UserTypes.Where(u => u.Id == issue.AssignedId).Select(u => u.Role).FirstOrDefault();
            string posName = Context.Pos.Where(p => p.Id == issue.PosId).Select(p => p.Name).FirstOrDefault();

            issueDTO.Id = issue.Id;
            issueDTO.Type = Context.IssueNames.Where(n => n.Id == issue.TypeId).Select(n => n.Name).FirstOrDefault();
            issueDTO.SubType = Context.IssueNames.Where(n => n.Id == issue.SubTypeId).Select(n => n.Name).FirstOrDefault();
            issueDTO.Problem = Context.IssueNames.Where(n => n.Id == issue.ProblemId).Select(n => n.Name).FirstOrDefault();
            issueDTO.Status = status;
            issueDTO.Priority = priority;
            issueDTO.Memo = issue.Memo;
            issueDTO.UserCreated = userCreated;
            issueDTO.AssignedTo = assignedTo;
            issueDTO.Description = issue.Description;
            issueDTO.AssignedDate = issue.AssignedDate.ToString();
            issueDTO.CreationDate = issue.CreationDate.ToString();
            issueDTO.ModificationDate = issue.ModificationDate.ToString();
            issueDTO.Solution = issue.Solution;

            return issueDTO;
        }

        public async Task<List<IssueGetDTO>> GetIssuesByPosIdAsync(int posId)
        {
            var rawIssueList = await Context.Issues
                .Include(i => i.Pos)
                .Include(i => i.Status)
                .Include(i => i.User)
                .Include(i => i.UserType)
                .Include(i => i.IssueType)
                .Include(i => i.Priority)
                .Include(i => i.IssueSubType)
                .Include(i => i.IssueProblem)
                .Where(i => i.PosId == posId && i.IsDeleted == false)
                .ToListAsync();

            List<IssueGetDTO> issueList = null;
            try
            {
                issueList = rawIssueList
                    .Select(i => new IssueGetDTO
                    {
                        Id = i.Id,
                        Priority = i.Priority.IssuePriority,
                        Type = Context.IssueNames.Where(n => n.Id == i.TypeId).Select(n => n.Name).FirstOrDefault(),
                        SubType = Context.IssueNames.Where(n => n.Id == i.SubTypeId).Select(n => n.Name).FirstOrDefault(),
                        Problem = Context.IssueNames.Where(n => n.Id == i.ProblemId).Select(n => n.Name).FirstOrDefault(),
                        Memo = i.Memo,
                        Status = i.Status.IssueStatus,
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
                    .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return issueList;
        }

        public async Task<List<StatusDTO>> GetStatusList()
        {
            return await Context.Statuses
                .Select(s => new StatusDTO
                {
                    Id = s.Id,
                    IssueStatus = s.IssueStatus
                })
                .ToListAsync();
        }

        public async Task<List<PriorityDTO>> GetPriorityList()
        {
            return await Context.Priorities
                .Select(p => new PriorityDTO
                {
                    Id = p.Id,
                    IssuePriority = p.IssuePriority
                })
                .ToListAsync();
        }

        public async Task<List<IssueNameDTO>> GetIssueNameList(int? id)
        {
            return await Context.IssueNames
                .Where(i => i.ParentId == id)
                .Select(i => new IssueNameDTO
                {
                    Id = i.Id,
                    IssueName = i.Name
                })
                .ToListAsync();
        }

        public async Task<StatusGroupCount> GetStatusGroupCount()
        {
            var allissues = await Context.Issues
                .Where(i => i.IsDeleted == false)
                .Include(i => i.Status).ToListAsync();

            int newIssueCount = allissues.Where(i => i.Status.IssueStatus == "New").Count();
            int assignedIssueCount = allissues.Where(i => i.Status.IssueStatus == "Assigned").Count();
            int inprogressIssueCount = allissues.Where(i => i.Status.IssueStatus == "In progress").Count();
            int pendingIssueCount = allissues.Where(i => i.Status.IssueStatus == "Pending").Count();

            return new StatusGroupCount
            {
                NewIssues = newIssueCount,
                AssignedIssues = assignedIssueCount,
                InProgressIssues = inprogressIssueCount,
                PendingIssues = pendingIssueCount
            };
        }
    }
}