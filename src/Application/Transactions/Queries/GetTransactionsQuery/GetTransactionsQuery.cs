using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ing.Interview.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ing.Interview.Application.Transactions.Queries.GetTransactionsQuery
{
    public class GetTransactionsQuery : IRequest<List<TransactionItem>>
    {
        public string AccountNumber { get; set; }
    }

    public class TransactionItem
    {
        private TransactionItem(int category, decimal totalAmount, string currency)
        {
            CategoryName = category.ToString();
            TotalAmount = totalAmount;
            Currency = currency;
        }

        public string CategoryName { get; }
        public decimal TotalAmount { get; }
        public string Currency { get; }

        public static TransactionItem From(int category, decimal totalAmount, string currency) => new TransactionItem(category, totalAmount, currency);
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

        public async Task<List<TransactionItem>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var account = _context.Accounts.First(a => a.AccountNumber.Equals(request.AccountNumber));
            var transactions = await _context.Transactions
                .Where(t => t.AccountNumber.Equals(request.AccountNumber))
                .Where(t => t.TransactionDate >= _dateTime.Now.AddMonths(-1))
                .ToListAsync(cancellationToken);
            var transactionsGroupedByCategory = transactions.GroupBy(t => t.CategoryId);
            var items = transactionsGroupedByCategory
                .Select(g => TransactionItem.From(g.Key, g.Sum(i => i.Amount), account.Currency))
                .ToList();
            return items;
        }
    }
}
