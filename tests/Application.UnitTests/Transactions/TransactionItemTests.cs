using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Ing.Interview.Application.Accounts.Queries.GetAccountsQuery;
using Ing.Interview.Domain.Entities;
using NUnit.Framework;

namespace Ing.Interview.Application.UnitTests.Transactions
{
    public class TransactionItemTests
    {
        [Test]
        public void PassListOfAccount_AccountsPropertyIsPopulated()
        {
            // arrange
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

            // act 
            var sut = GetAccountsResult.From(accounts);

            // assert

            sut.Accounts.Should().NotBeEmpty();
            sut.Accounts
                .All(i => accounts.Any(a => i.AccountNumber.Equals(a.AccountNumber)))
                .Should()
                .BeTrue();
        }
    }
}