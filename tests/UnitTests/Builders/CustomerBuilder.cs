using CoreBanking.Domain.Entities;

namespace UnitTests.Builders
{
    public class CustomerBuilder
    {
        private Customer _customer;

        public string NationalCode => "0024264788";
        public string FirstName => "Ehsan";
        public string LastName => "Arefzadeh";
        public Guid UserId => new Guid("4692D21F-3B31-4B41-A5D4-2A9AF937147E");

        public CustomerBuilder()
        {
            _customer = WithDefaultValue();
        }

        public Customer Build()
        {
            return _customer;
        }

        public Customer WithDefaultValue()
        {
            return _customer = Customer.Create(NationalCode, FirstName, LastName, UserId);
        }
    }
}
