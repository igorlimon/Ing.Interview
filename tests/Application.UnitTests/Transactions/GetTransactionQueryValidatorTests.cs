using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Ing.Interview.Application.Common.Interfaces;
using Ing.Interview.Application.Transactions.Queries.GetTransactionsQuery;
using Ing.Interview.Domain.Entities;
using Ing.Interview.Domain.ValueObjects;
using Moq;
using NUnit.Framework;

namespace Ing.Interview.Application.UnitTests.Transactions
{
    public class GetTransactionQueryValidatorTests
    {
        [Test]
        public void EmptyValueIsPassed_ValidationFailedWithExpectedErrorIsReturned()
        {
            // arrange
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
            var sut = new GetTransactionQueryValidator(mockApplicationDbContext.Object);

            // act
            var instanceToValidate = new GetTransactionsQuery();
            var validationResult = sut.Validate(instanceToValidate);

            // assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Any(e => e.ErrorMessage == "Account Number is required.").Should().BeTrue();
        }

        [Test]
        public void AccountNumberContainsOtherCharactersBesideAlphanumeric_ValidationFailedWithExpectedErrorIsReturned()
        {
            // arrange
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
            var sut = new GetTransactionQueryValidator(mockApplicationDbContext.Object);

            // act
            var instanceToValidate = new GetTransactionsQuery()
            {
                AccountNumber = "A1!"
            };
            var validationResult = sut.Validate(instanceToValidate);

            // assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Any(e => e.ErrorMessage == "Only alphanumeric characters are allowed.").Should().BeTrue();
        }

        [Test]
        public void AccountNotExist_ValidationFailedWithExpectedErrorIsReturned()
        {
            // arrange
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
            var sut = new GetTransactionQueryValidator(mockApplicationDbContext.Object);

            // act
            var instanceToValidate = new GetTransactionsQuery()
            {
                AccountNumber = "A1"
            };
            var validationResult = sut.Validate(instanceToValidate);

            // assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Any(e => e.ErrorMessage == "Account with specified number does not exist.").Should().BeTrue();
        }

        [Test]
        public void PassValidAccountNumber_ValidationSucceded()
        {
            // arrange
            var mockApplicationDbContext = new Mock<IApplicationDbContext>();
            var accountNumber = "ABC123";
            Account account = new()
            {
                AccountNumber = accountNumber,
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
            var sut = new GetTransactionQueryValidator(mockApplicationDbContext.Object);

            // act
            var instanceToValidate = new GetTransactionsQuery()
            {
                AccountNumber = accountNumber
            };
            var validationResult = sut.Validate(instanceToValidate);

            // assert
            validationResult.IsValid.Should().BeTrue();
        }
    }
}
