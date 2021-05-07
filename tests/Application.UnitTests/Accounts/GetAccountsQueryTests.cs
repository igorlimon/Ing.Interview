using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Ing.Interview.Application.Accounts.Queries.GetAccountsQuery;
using Ing.Interview.Application.Common.Interfaces;
using Ing.Interview.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace Ing.Interview.Application.UnitTests.Accounts
{
    public class GetAccountsQueryTests
    {
        [Test]
        public async Task AccountsNotExist_ReturnEmptyList()
        {
            // arrange
            var mockApplicationDbContext = new Mock<IApplicationDbContext>();
            var stubListOfEntities = new StubListOfEntities<Account>(new List<Account>());
            mockApplicationDbContext
                .Setup(m => m.Accounts)
                .Returns(stubListOfEntities);

            var sut = new GetAccountsQueryHandler(mockApplicationDbContext.Object);

            // act
            var result = await sut.Handle(new GetAccountsQuery(), CancellationToken.None);

            // assert
            result.Accounts.Should().BeEmpty();
        }

        [Test]
        public async Task AccountsExist_ListWithAllAccountIsReturned()
        {
            // arrange
            var mockApplicationDbContext = new Mock<IApplicationDbContext>();
            List<Account> accounts = new List<Account>()
            {
                new()
                {
                    AccountNumber = "ABC123"
                },
                new()
                {
                    AccountNumber = "DEF123"
                }
            };
            var stubListOfEntities = new StubListOfEntities<Account>(accounts);
            mockApplicationDbContext
                .Setup(m => m.Accounts)
                .Returns(stubListOfEntities);

            var sut = new GetAccountsQueryHandler(mockApplicationDbContext.Object);

            // act
            var result = await sut.Handle(new GetAccountsQuery(), CancellationToken.None);

            // assert
            result.Accounts.Should().NotBeEmpty();
            result.Accounts
                .All(i => accounts.Any(a => i.AccountNumber.Equals(a.AccountNumber)))
                .Should()
                .BeTrue();
        }
    }
}
