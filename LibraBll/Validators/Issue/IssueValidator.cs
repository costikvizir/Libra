using FluentValidation;
using LibraBll.Abstractions.Repositories;
using LibraBll.DTOs.Issue;
using LibraBll.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraBll.Validators.Issue
{
	public class IssueValidator : AbstractValidator<IssuePostDTO>
	{
		private readonly IIssueRepository _issueRepository;
        public IssueValidator(IIssueRepository issueRepository)
        {
			_issueRepository = issueRepository;

			//RuleFor(i => i.Type)
			//	.NotEmpty().WithMessage("Type is required")
   //              .MustAsync(async (type, cancellation) =>
   //              {
   //                  var roles = await _issueRepository.GetType();
   //                  return roles.Any(r => r.Id == role);
   //              }).WithMessage("Please select a valid role!");

            //RuleFor(i => i.Type).NotEmpty().WithMessage("Type is required");
            RuleFor(i => i.Priority).NotEmpty().WithMessage("Priority is required");
			RuleFor(i => i.Status).NotEmpty().WithMessage("Status is required");
			//RuleFor(i => i.Memo).NotEmpty().WithMessage("Memo is required");
			RuleFor(i => i.UserCreated).NotEmpty().WithMessage("User is required");
			RuleFor(i => i.AssignedTo).NotEmpty().WithMessage("Departament to assign is required");
			RuleFor(i => i.Description).NotEmpty().WithMessage("Description is required");
			//RuleFor(i => i.AssignedDate).NotEmpty().WithMessage("AssignedDate is required");
			//RuleFor(i => i.ModificationDate).NotEmpty().WithMessage("ModificationDate is required");
			//RuleFor(i => i.Solution).NotEmpty().WithMessage("Solution is required");	
        }
    }
}
