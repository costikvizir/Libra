using LibraBll.Abstractions.Repositories;
using LibraBll.Abstractions.ValidatorFactory;
using LibraBll.Validators.Issue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraBll.Validators.ValidatorFactory
{
    public class IssueValidatorFactory : IIssueValidatorFactory
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IUserRepository _userRepository;

        public IssueValidatorFactory(IIssueRepository issueRepository, IUserRepository userRepository)
        {
            _issueRepository = issueRepository;
            _userRepository = userRepository;
        }

        public IssueValidator Create(int issueSubType, int issueProblem, int? issueType = null)
        {
            return new IssueValidator(_issueRepository, _userRepository, issueSubType, issueProblem, issueType);
        }
    }
}