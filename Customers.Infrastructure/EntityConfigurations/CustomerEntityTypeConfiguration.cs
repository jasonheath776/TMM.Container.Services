using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers.Infrastructure.EntityConfigurations
{
    class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> customerConfiguration)
        {
            customerConfiguration.ToTable("Customers", CustomerContext.DEFAULT_SCHEMA);

            customerConfiguration.HasKey(x => x.CustomerId);
            customerConfiguration.Ignore(x => x.DomainEvents);

            customerConfiguration.HasMany(s => s.Addresses)
           .WithOne()
           .HasForeignKey(s => s.CustomerId)
           .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
