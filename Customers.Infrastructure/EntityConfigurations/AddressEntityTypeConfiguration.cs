using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers.Infrastructure.EntityConfigurations
{
    public class AddressEntityTypeConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> addressConfiguration)
        {
            addressConfiguration.ToTable("Address", CustomerContext.DEFAULT_SCHEMA);

            addressConfiguration.HasKey(x => x.AddressId);
            addressConfiguration.HasIndex(s => s.CustomerId);
        }
    }
}
