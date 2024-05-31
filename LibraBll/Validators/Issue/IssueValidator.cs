using FluentValidation;
using LibraBll.Abstractions.Repositories;
using LibraBll.DTOs.Issue;
using System.Linq;

namespace LibraBll.Validators.Issue
{
    public class IssueValidator : AbstractValidator<IssuePostDTO>
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IUserRepository _userRepository;

        public IssueValidator(IIssueRepository issueRepository, IUserRepository userRepository, int issueSubType, int issueProblem, int? issueType = null)
        {
            _issueRepository = issueRepository;
            _userRepository = userRepository;

            RuleFor(i => i.Type)
                .NotEmpty().WithMessage("Type is required")
                 .MustAsync(async (type, cancellation) =>
                 {
                     var types = await _issueRepository.GetIssueNameList(issueType);
                     return types.Any(t => t.Id == type);
                 }).WithMessage("Please select a valid type!");

            RuleFor(i => i.SubType)
                .NotEmpty().WithMessage("Type is required")
                 .MustAsync(async (subType, cancellation) =>
                 {
                     var subTypes = await _issueRepository.GetIssueNameList(issueSubType);
                     return subTypes.Any(t => t.Id == subType);
                 }).WithMessage("Please select a valid Subtype!");

            RuleFor(i => i.Problem)
                .NotEmpty().WithMessage("Type is required")
                 .MustAsync(async (problem, cancellation) =>
                 {
                     var problems = await _issueRepository.GetIssueNameList(issueProblem);
                     return problems.Any(t => t.Id == problem);
                 }).WithMessage("Please select a valid problem!");

            RuleFor(i => i.Priority)
                .NotEmpty().WithMessage("Type is required")
                 .MustAsync(async (priority, cancellation) =>
                 {
                     var priorities = await _issueRepository.GetPriorityList();
                     return priorities.Any(t => t.Id == priority);
                 }).WithMessage("Please select a priority!");

            RuleFor(i => i.Status)
                .NotEmpty().WithMessage("Type is required")
                 .MustAsync(async (status, cancellation) =>
                 {
                     var statuses = await _issueRepository.GetStatusList();
                     return statuses.Any(t => t.IssueStatus == status);
                 }).WithMessage("Please select a status!");

            RuleFor(i => i.AssignedTo)
                .NotEmpty().WithMessage("Type is required")
                 .MustAsync(async (user, cancellation) =>
                 {
                     var users = await _userRepository.GetRoles();
                     return users.Any(t => t.Id == user);
                 }).WithMessage("Please select a valid role!");

            RuleFor(i => i.Description).NotEmpty().WithMessage("Description is required");
        }
    }
}