using Customers.Domain.Events;
using Customers.Infrastructure.Repositories;

#nullable disable

namespace Customers.API.Application.DomainEventHandlers
{
    public class CustomerCreatedEventHandler
                    : INotificationHandler<CustomerCreatedDomainEvent>
    {
        private readonly ILoggerFactory _logger;
        private readonly ICustomerRepository _customerRepository;

        public CustomerCreatedEventHandler(
            ILoggerFactory logger,
            CustomerRepository customerRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _customerRepository = customerRepository;
        }

        public Task Handle(CustomerCreatedDomainEvent customerCreatedDomainEvent, CancellationToken cancellationToken)
        {
            _logger.CreateLogger<CustomerCreatedDomainEvent>()
             .LogTrace("Customer has been created id {Id}",
                 customerCreatedDomainEvent.Customer.Id);
            _customerRepository.Add(customerCreatedDomainEvent.Customer);

            return Task.CompletedTask;
        }
    }
}
