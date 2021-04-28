using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ing.Interview.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ing.Interview.Application.Transactions.Queries.GetTransactionsQuery
{
    public class GetTransactionsQuery : IRequest<GetTransactionsResult>
    {
        public string Iban { get; set; }
    }

    public class GetTransactionsResult
    {
        private GetTransactionsResult(List<Item> items) => Items = items;

        public List<Item> Items { get; }

        public static GetTransactionsResult From(List<Item> items) => new GetTransactionsResult(items);

        public class Item
        {
            private Item(int category, decimal totalAmount, string currency)
            {
                CategoryName = category.ToString();
                TotalAmount = totalAmount;
                Currency = currency;
            }

            public string CategoryName { get; }
            public decimal TotalAmount { get; }
            public string Currency { get; }

            public static Item From(int category, decimal totalAmount, string currency) => new Item(category, totalAmount, currency);
        }
    }

    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, GetTransactionsResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _dateTime;

        public GetTransactionsQueryHandler(IApplicationDbContext context, IDateTime dateTime)
        {
            _context = context;
            _dateTime = dateTime;
        }

        public async Task<GetTransactionsResult> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.Iban.Equals(request.Iban));
            if (account == null)
            {
                return GetTransactionsResult.From(new List<GetTransactionsResult.Item>());
            }

            var transactions = await _context.Transactions
                .Where(t => t.Iban.Equals(request.Iban))
                .Where(t => t.TransactionDate >= _dateTime.Now.AddMonths(-1))
                .GroupBy(t => t.CategoryId)
                .ToListAsync(cancellationToken);
            var items = transactions.Select(g =>
                GetTransactionsResult.Item.From(g.Key, g.Sum(i => i.Amount), account.Currency)).ToList();
            return GetTransactionsResult.From(items);
        }
    }
}
