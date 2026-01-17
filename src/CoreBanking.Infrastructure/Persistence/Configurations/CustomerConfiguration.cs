using CoreBanking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CoreBanking.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Customer.Accounts));

            navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasQueryFilter(c => !c.IsDeleted);

        }
    }
}
