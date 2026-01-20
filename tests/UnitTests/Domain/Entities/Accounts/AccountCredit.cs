using CoreBanking.Domain.Enums;
using CoreBanking.Domain.Exceptions;
using UnitTests.Builders;

namespace UnitTests.Domain.Entities.Accounts
{
    public class AccountCredit
    {
        [Fact]
        public void ShouldCreditBalanceWhenAmountIsValid()
        {
            //Arrange
            var account = new AccountBuilder().WithDefaultValue();

            //Act
            account.Credit(100);

            //Assert
            Assert.Equal(100, account.Balance);
        }
        [Fact]
        public void ShouldThrowExceptionWhenAccountStatusIsNotActive()
        {
            //Arrange
            var account = new AccountBuilder().WithDefaultValue();

            //Act
            account.ChangeStatus(AccountStatus.Blocked);
            Action action = () => account.Credit(100);

            //Assert
            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void ShouldThrowExceptionWhenAmountIsNotValid()
        {
            //Arrange
            var account = new AccountBuilder().WithDefaultValue();

            //Act
            Action action = () => account.Credit(0);

            //Assert
            Assert.Throws<DomainException>(action);
        }
    }
}
