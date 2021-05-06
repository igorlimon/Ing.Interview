using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Ing.Interview.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                .MustAsync(BeUniqueTitle).WithMessage("Account with specified number does not exist.");
        }

        public async Task<bool> BeUniqueTitle(string accountNumber, CancellationToken cancellationToken)
        {
            return await _context.Accounts
                .AnyAsync(l => l.AccountNumber == accountNumber, cancellationToken);
        }
    }
}
