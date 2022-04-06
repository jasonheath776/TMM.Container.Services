namespace Customers.Domain.AggregatesModel.CustomerAggregate
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer Add(Customer customer);

        Customer Update(Customer customer);

        Task<Customer> GetAsync(int CustomerId);
    }
}
