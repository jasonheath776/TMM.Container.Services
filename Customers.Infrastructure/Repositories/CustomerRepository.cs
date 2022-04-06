#nullable disable 

namespace Customers.Infrastructure.Repositories
{
    public class CustomerRepository
        : ICustomerRepository
    {
        private readonly CustomerDbContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public CustomerRepository(CustomerDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Customer Add(Customer customer)
        {
            if (customer.IsTransient())
            {
                return _context.Customers
                    .Add(customer)
                    .Entity;
            }
            else
            {
                return customer;
            }
        }

        public async Task<Customer> GetAsync(int customerId)
        {
            var customer = await _context.Customers
                .Include(b => b.Addresses)
                .Where(b => b.Id == customerId)
                .SingleOrDefaultAsync();

            return customer;
        }

        public Customer Update(Customer customer)
        {
            return _context.Customers
                .Update(customer)
                .Entity;
        }
    }
}
