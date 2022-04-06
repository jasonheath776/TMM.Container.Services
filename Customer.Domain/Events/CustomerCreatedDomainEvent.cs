using Customers.Domain.AggregatesModel.CustomerAggregate;

#nullable disable

namespace Customers.Domain.Events
{

    public class CustomerCreatedDomainEvent : INotification
    {
        public Customer Customer { get; }

        public CustomerCreatedDomainEvent(Customer customer)
        {
            Customer = customer;
        }
    }
}
