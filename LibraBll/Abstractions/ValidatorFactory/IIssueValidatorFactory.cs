using LibraBll.Validators.Issue;

namespace LibraBll.Abstractions.ValidatorFactory
{
    public interface IIssueValidatorFactory
    {
        IssueValidator Create(int issueSubType, int issueProblem, int? issueType = null);
    }
}