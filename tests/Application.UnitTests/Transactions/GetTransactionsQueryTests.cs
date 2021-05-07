using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Ing.Interview.Application.Common.Interfaces;
using Ing.Interview.Application.Transactions.Queries.GetTransactionsQuery;
using Ing.Interview.Domain.Entities;
using Ing.Interview.Domain.Enums;
using Ing.Interview.Domain.ValueObjects;
using Moq;
using NUnit.Framework;

namespace Ing.Interview.Application.UnitTests.Transactions
{
    public class GetTransactionsQueryTests
    {
        [Test]
        public async Task TransactionForSpecifiedAccountNotExist_ReturnEmptyList()
        {
            // arrange
            var mockDateTime = new Mock<IDateTime>();
            var systemTime = DateTime.UtcNow;
            mockDateTime
                .Setup(m => m.Now)
                .Returns(systemTime);
            var mockApplicationDbContext = new Mock<IApplicationDbContext>();
            var accountNumber = "ABC123";
            List<Account> accounts = new List<Account>()
            {
                new()
                {
                    AccountNumber = accountNumber
                },
                new()
                {
                    AccountNumber = "DEF123"
                }
            };

            var stubAccounts = new StubListOfEntities<Account>(accounts);
            mockApplicationDbContext
                .Setup(m => m.Accounts)
                .Returns(stubAccounts);
            var transactionDateNotOldThenOneMonth = systemTime.AddHours(-3);
            var transactions = new List<Transaction>()
            {
                new()
                {
                    AccountNumber = "XYZ123",
                    TransactionDate = transactionDateNotOldThenOneMonth
                }
            };
            var stubTransactions= new StubListOfEntities<Transaction>(transactions);
            mockApplicationDbContext
                .Setup(m => m.Transactions)
                .Returns(stubTransactions);


            var sut = new GetTransactionsQueryHandler(mockApplicationDbContext.Object, mockDateTime.Object);

            // act
            var result = await sut.Handle(new GetTransactionsQuery() {AccountNumber = accountNumber}, CancellationToken.None);

            // assert
            result.Should().BeEmpty();
        }

        [Test]
        public async Task TransactionExistForSpecifiedAccountButAreTooOld_ReturnEmptyList()
        {

            // arrange
            var mockDateTime = new Mock<IDateTime>();
            var systemTime = DateTime.UtcNow;
            mockDateTime
                .Setup(m => m.Now)
                .Returns(systemTime);
            var mockApplicationDbContext = new Mock<IApplicationDbContext>();
            var accountNumber = "ABC123";
            List<Account> accounts = new List<Account>()
            {
                new()
                {
                    AccountNumber = accountNumber
                },
                new()
                {
                    AccountNumber = "DEF123"
                }
            };

            var stubAccounts = new StubListOfEntities<Account>(accounts);
            mockApplicationDbContext
                .Setup(m => m.Accounts)
                .Returns(stubAccounts);
            var transactionDateNotOldThenOneMonth = systemTime.AddMonths(-2);
            var transactions = new List<Transaction>()
            {
                new()
                {
                    AccountNumber = accountNumber,
                    TransactionDate = transactionDateNotOldThenOneMonth
                }
            };
            var stubTransactions = new StubListOfEntities<Transaction>(transactions);
            mockApplicationDbContext
                .Setup(m => m.Transactions)
                .Returns(stubTransactions);


            var sut = new GetTransactionsQueryHandler(mockApplicationDbContext.Object, mockDateTime.Object);

            // act
            var result = await sut.Handle(new GetTransactionsQuery() { AccountNumber = accountNumber }, CancellationToken.None);

            // assert
            result.Should().BeEmpty();
        }

        [Test]
        public async Task RecentOnlyOneTransactionExistForSpecifiedAccount_TransactionAreReturned()
        {

            // arrange
            var mockDateTime = new Mock<IDateTime>();
            var systemTime = DateTime.UtcNow;
            mockDateTime
                .Setup(m => m.Now)
                .Returns(systemTime);
            var mockApplicationDbContext = new Mock<IApplicationDbContext>();
            Account account = new()
            {
                AccountNumber = "ABC123",
                Currency = Currency.EUR
            };
            List<Account> accounts = new List<Account>()
            {
                account,
                new()
                {
                    AccountNumber = "DEF123"
                }
            };

            var stubAccounts = new StubListOfEntities<Account>(accounts);
            mockApplicationDbContext
                .Setup(m => m.Accounts)
                .Returns(stubAccounts);
            var transactionDateNotOldThenOneMonth = systemTime.AddHours(-2);
            var transactions = new List<Transaction>()
            {
                new()
                {
                    AccountNumber = account.AccountNumber,
                    TransactionDate = transactionDateNotOldThenOneMonth,
                    Amount = 200,
                    CategoryId = (int) TransactionCategory.Clothing,
                    TransactionId = 10
                }
            };
            var stubTransactions = new StubListOfEntities<Transaction>(transactions);
            mockApplicationDbContext
                .Setup(m => m.Transactions)
                .Returns(stubTransactions);


            var sut = new GetTransactionsQueryHandler(mockApplicationDbContext.Object, mockDateTime.Object);

            // act
            var result = await sut.Handle(new GetTransactionsQuery() { AccountNumber = account.AccountNumber }, CancellationToken.None);

            // assert
            result.Should().NotBeNullOrEmpty();
            result.Count.Should().Be(1);
            result.Any(a => a.CategoryName == TransactionCategory.Clothing.ToString()).Should().BeTrue();
            result.Any(a => a.TotalAmount == 200).Should().BeTrue();
        }

        [Test]
        public async Task MoreTransactionWithDifferentCategoryExistForSpecifiedAccount_TransactionGroupedByCategoryAreReturned()
        {

            // arrange
            var mockDateTime = new Mock<IDateTime>();
            var systemTime = DateTime.UtcNow;
            mockDateTime
                .Setup(m => m.Now)
                .Returns(systemTime);
            var mockApplicationDbContext = new Mock<IApplicationDbContext>();
            Account account = new()
            {
                AccountNumber = "ABC123",
                Currency = Currency.EUR
            };
            List<Account> accounts = new List<Account>()
            {
                account,
                new()
                {
                    AccountNumber = "DEF123"
                }
            };

            var stubAccounts = new StubListOfEntities<Account>(accounts);
            mockApplicationDbContext
                .Setup(m => m.Accounts)
                .Returns(stubAccounts);
            var transactionDateNotOldThenOneMonth = systemTime.AddHours(-2);
            var transactions = new List<Transaction>()
            {
                new()
                {
                    AccountNumber = account.AccountNumber,
                    TransactionDate = transactionDateNotOldThenOneMonth,
                    Amount = 200,
                    CategoryId = (int) TransactionCategory.Clothing,
                    TransactionId = 10
                },
                new()
                {
                    AccountNumber = account.AccountNumber,
                    TransactionDate = transactionDateNotOldThenOneMonth,
                    Amount = 300,
                    CategoryId = (int) TransactionCategory.Entertainment,
                    TransactionId = 11
                }
            };
            var stubTransactions = new StubListOfEntities<Transaction>(transactions);
            mockApplicationDbContext
                .Setup(m => m.Transactions)
                .Returns(stubTransactions);


            var sut = new GetTransactionsQueryHandler(mockApplicationDbContext.Object, mockDateTime.Object);

            // act
            var result = await sut.Handle(new GetTransactionsQuery() { AccountNumber = account.AccountNumber }, CancellationToken.None);

            // assert
            result.Should().NotBeNullOrEmpty();
            result.Count.Should().Be(2);
            result.Any(a => a.CategoryName == TransactionCategory.Clothing.ToString()).Should().BeTrue();
            result.Any(a => a.TotalAmount == 200).Should().BeTrue();
            result.Any(a => a.CategoryName == TransactionCategory.Entertainment.ToString()).Should().BeTrue();
            result.Any(a => a.TotalAmount == 300).Should().BeTrue();
        }

        [Test]
        public async Task MoreTransactionExistForSpecifiedAccount_SameCurrencyIsReturnedForAllItems()
        {

            // arrange
            var mockDateTime = new Mock<IDateTime>();
            var systemTime = DateTime.UtcNow;
            mockDateTime
                .Setup(m => m.Now)
                .Returns(systemTime);
            var mockApplicationDbContext = new Mock<IApplicationDbContext>();
            Account account = new()
            {
                AccountNumber = "ABC123",
                Currency = Currency.EUR
            };
            List<Account> accounts = new List<Account>()
            {
                account,
                new()
                {
                    AccountNumber = "DEF123"
                }
            };

            var stubAccounts = new StubListOfEntities<Account>(accounts);
            mockApplicationDbContext
                .Setup(m => m.Accounts)
                .Returns(stubAccounts);
            var transactionDateNotOldThenOneMonth = systemTime.AddHours(-2);
            var transactions = new List<Transaction>()
            {
                new()
                {
                    AccountNumber = account.AccountNumber,
                    TransactionDate = transactionDateNotOldThenOneMonth,
                    Amount = 200,
                    CategoryId = (int) TransactionCategory.Clothing,
                    TransactionId = 10
                },
                new()
                {
                    AccountNumber = account.AccountNumber,
                    TransactionDate = transactionDateNotOldThenOneMonth,
                    Amount = 300,
                    CategoryId = (int) TransactionCategory.Entertainment,
                    TransactionId = 11
                }
            };
            var stubTransactions = new StubListOfEntities<Transaction>(transactions);
            mockApplicationDbContext
                .Setup(m => m.Transactions)
                .Returns(stubTransactions);


            var sut = new GetTransactionsQueryHandler(mockApplicationDbContext.Object, mockDateTime.Object);

            // act
            var result = await sut.Handle(new GetTransactionsQuery() { AccountNumber = account.AccountNumber }, CancellationToken.None);

            // assert
            result.Should().NotBeNullOrEmpty();
            result.All(a => a.Currency == account.Currency).Should().BeTrue();
        }

        [Test]
        public async Task MoreTransactionWithSameCategoryExistForSpecifiedAccount_OneItemWithTotalAmmountIsReturned()
        {

            // arrange
            var mockDateTime = new Mock<IDateTime>();
            var systemTime = DateTime.UtcNow;
            mockDateTime
                .Setup(m => m.Now)
                .Returns(systemTime);
            var mockApplicationDbContext = new Mock<IApplicationDbContext>();
            Account account = new()
            {
                AccountNumber = "ABC123",
                Currency = Currency.EUR
            };
            List<Account> accounts = new List<Account>()
            {
                account,
                new()
                {
                    AccountNumber = "DEF123"
                }
            };

            var stubAccounts = new StubListOfEntities<Account>(accounts);
            mockApplicationDbContext
                .Setup(m => m.Accounts)
                .Returns(stubAccounts);
            var transactionDateNotOldThenOneMonth = systemTime.AddHours(-2);
            var transactions = new List<Transaction>()
            {
                new()
                {
                    AccountNumber = account.AccountNumber,
                    TransactionDate = transactionDateNotOldThenOneMonth,
                    Amount = 200,
                    CategoryId = (int) TransactionCategory.Entertainment,
                    TransactionId = 10
                },
                new()
                {
                    AccountNumber = account.AccountNumber,
                    TransactionDate = transactionDateNotOldThenOneMonth,
                    Amount = 300,
                    CategoryId = (int) TransactionCategory.Entertainment,
                    TransactionId = 11
                }
            };
            var stubTransactions = new StubListOfEntities<Transaction>(transactions);
            mockApplicationDbContext
                .Setup(m => m.Transactions)
                .Returns(stubTransactions);


            var sut = new GetTransactionsQueryHandler(mockApplicationDbContext.Object, mockDateTime.Object);

            // act
            var result = await sut.Handle(new GetTransactionsQuery() { AccountNumber = account.AccountNumber }, CancellationToken.None);

            // assert
            result.Should().NotBeNullOrEmpty();
            result.Count.Should().Be(1);
            result.Any(a => a.CategoryName == TransactionCategory.Entertainment.ToString()).Should().BeTrue();
            result.Any(a => a.TotalAmount == 500).Should().BeTrue();
        }

    }
}
