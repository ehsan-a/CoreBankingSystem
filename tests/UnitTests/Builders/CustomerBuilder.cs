using CoreBanking.Domain.Entities;

namespace UnitTests.Builders
{
    public class CustomerBuilder
    {
        private Customer _customer;

        public string NationalCode => "0024264788";
        public string FirstName => "Ehsan";
        public string LastName => "Arefzadeh";

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
            return _customer = new Customer(NationalCode, FirstName, LastName);
        }
    }
}
