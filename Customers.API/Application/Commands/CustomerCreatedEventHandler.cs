using Customers.Domain.Events;

#nullable disable

namespace Customers.API.Application.Commands
{
    public class CreateCustomerCommandHandler
                    : IRequestHandler<CreateCustomerCommand, Customer>
    {
        //private readonly ILoggerFactory _logger;
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            //_logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _customerRepository = customerRepository;
        }


        public async Task<Customer> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            Customer customer = new Customer
            {
                EmailAddress = command.EmailAddress,
                Forename = command.Forename,
                Surname = command.Surname,
                IsActive = command.IsActive,
                MobileNo = command.MobileNo,
                Title = command.Title,
            };


            _customerRepository.Add(customer);
            await _customerRepository.UnitOfWork.SaveChangesAsync();
            return customer;
        }
    }
}
