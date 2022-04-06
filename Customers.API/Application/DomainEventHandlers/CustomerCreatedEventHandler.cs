using Customers.Domain.Events;

namespace Customers.API.Application.DomainEventHandlers
{
    public class CustomerCreatedEventHandler : INotificationHandler<CustomerCreatedDomainEvent>
    {
        private readonly ILoggerFactory _logger;

        public CustomerCreatedEventHandler(ILoggerFactory logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(CustomerCreatedDomainEvent customerCreatedDomainEvent, CancellationToken cancellationToken)
        {
            _logger.CreateLogger<CustomerCreatedDomainEvent>()
             .LogTrace("Customer has been created id {}",
                 customerCreatedDomainEvent.Customer.Id);
        }
    }
}
