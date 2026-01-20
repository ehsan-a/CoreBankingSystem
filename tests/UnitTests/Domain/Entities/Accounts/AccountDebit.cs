using CoreBanking.Domain.Enums;
using CoreBanking.Domain.Exceptions;
using UnitTests.Builders;

namespace UnitTests.Domain.Entities.Accounts
{
    public class AccountDebit
    {

        [Fact]
        public void ShouldDebitBalanceWhenAmountIsValid()
        {
            //Arrange
            var account = new AccountBuilder().WithDefaultValue();

            //Act
            account.Credit(100);
            account.Debit(100);

            //Assert
            Assert.Equal(0, account.Balance);
        }

        [Fact]
        public void ShouldThrowExceptionWhenAccountStatusIsNotActive()
        {
            //Arrange
            var account = new AccountBuilder().WithDefaultValue();

            //Act
            account.ChangeStatus(AccountStatus.Blocked);
            Action action = () => account.Debit(100);

            //Assert
            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void ShouldThrowExceptionWhenAmountIsNotValid()
        {
            //Arrange
            var account = new AccountBuilder().WithDefaultValue();

            //Act
            Action action = () => account.Debit(0);

            //Assert
            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void ShouldThrowExceptionWhenBalanceLessThanAmount()
        {
            //Arrange
            var account = new AccountBuilder().WithDefaultValue();

            //Act
            account.Credit(100);
            Action action = () => account.Debit(120);

            //Assert
            Assert.Throws<DomainException>(action);
        }
    }
}
