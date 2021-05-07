using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ing.Interview.Application.Common.Interfaces;
using Ing.Interview.Domain.Enums;
using MediatR;

namespace Ing.Interview.Application.Transactions.Queries.GetTransactionsQuery
{
    public class GetTransactionsQuery : IRequest<List<TransactionItem>>
    {
        public string AccountNumber { get; set; }
    }

    public class TransactionItem
    {
        private TransactionItem(string category, decimal totalAmount, string currency)
        {
            CategoryName = category;
            TotalAmount = totalAmount;
            Currency = currency;
        }

        public string CategoryName { get; }
        public decimal TotalAmount { get; }
        public string Currency { get; }

        public static TransactionItem From(string category, decimal totalAmount, string currency) => new(category, totalAmount, currency);
    }

    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, List<TransactionItem>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _dateTime;

        public GetTransactionsQueryHandler(IApplicationDbContext context, IDateTime dateTime)
        {
            _context = context;
            _dateTime = dateTime;
        }

        public Task<List<TransactionItem>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var account = _context.Accounts.First(a => a.AccountNumber == request.AccountNumber);
            var transactions = _context.Transactions
                .Where(t => t.AccountNumber.Equals(request.AccountNumber))
                .Where(t => t.TransactionDate >= _dateTime.Now.AddMonths(-1))
                .ToList();
            var transactionsGroupedByCategory = transactions.GroupBy(t => t.CategoryId);
            var items = transactionsGroupedByCategory
                .Select(g => TransactionItem.From(((TransactionCategory)g.Key).ToString(), g.Sum(i => i.Amount), account.Currency))
                .ToList();
            return Task.FromResult(items);
        }
    }
}
