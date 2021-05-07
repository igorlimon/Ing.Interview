using System.Linq;
using System.Text.RegularExpressions;
using FluentValidation;
using Ing.Interview.Application.Common.Interfaces;

namespace Ing.Interview.Application.Transactions.Queries.GetTransactionsQuery
{
    public class GetTransactionQueryValidator : AbstractValidator<GetTransactionsQuery>
    {
        private readonly IApplicationDbContext _context;

        public GetTransactionQueryValidator(IApplicationDbContext context)
        {
            _context = context;
            string pattern = "^[a-zA-Z0-9]*$";
            RuleFor(v => v.AccountNumber)
                .Cascade(CascadeMode.Stop)
                .Must(s => 
                    !string.IsNullOrWhiteSpace(s)).WithMessage("Account Number is required.")
                .Must(s => Regex.IsMatch(s, pattern)).WithMessage("Only alphanumeric characters are allowed.")
                .Must(AccountExist).WithMessage("Account with specified number does not exist.");
        }

        public bool AccountExist(string accountNumber)
        {
            return _context.Accounts
                .Any(l => l.AccountNumber == accountNumber);
        }
    }
}
