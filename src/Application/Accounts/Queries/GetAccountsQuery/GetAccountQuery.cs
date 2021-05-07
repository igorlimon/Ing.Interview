using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ing.Interview.Application.Common.Interfaces;
using Ing.Interview.Domain.Entities;
using MediatR;

namespace Ing.Interview.Application.Accounts.Queries.GetAccountsQuery
{
    public class GetAccountsQuery : IRequest<GetAccountsResult>
    {
    }

    public class GetAccountsResult
    {
        private GetAccountsResult(List<Account> accounts)
        {
            Accounts = accounts;
        }

        public List<Account> Accounts { get; }

        public static GetAccountsResult From(List<Account> accounts)
        {
            return new(accounts);
        }
    }

    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, GetAccountsResult>
    {
        private readonly IApplicationDbContext _context;

        public GetAccountsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task<GetAccountsResult> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = _context.Accounts.ToList();
            return Task.FromResult(GetAccountsResult.From(accounts));
        }
    }
}
