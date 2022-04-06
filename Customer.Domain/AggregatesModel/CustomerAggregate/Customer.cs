using Customers.Domain.Exceptions;
using Customers.Domain.SeedWork;

#nullable disable

namespace Customers.Domain.AggregatesModel.CustomerAggregate
{
    public class Customer : Entity, IAggregateRoot
    {
        public int CustomerId { get; private set; }

        //[Required]
        //[StringLength(20, ErrorMessage = "Title length can't be more than 20.")]
        public string Title { get; set; } = string.Empty;

        //[Required]
        //[StringLength(50, ErrorMessage = "Forename length can't be more than 50.")]
        public string Forename { get; set; } = string.Empty;

        //[Required]
        //[StringLength(50, ErrorMessage = "Surname length can't be more than 50.")]
        public string Surname { get; set; } = string.Empty;

        //[Required]
        //[EmailAddress]
        //[StringLength(75, ErrorMessage = "Email address length can't be more than 75.")]
        public string EmailAddress { get; set; } = string.Empty;

        //[Required]
        //[StringLength(15, ErrorMessage = "Phone number can't be more than 15.")]
        //[Phone]
        public string MobileNo { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        //[Address]
        private List<Address> _addresses;
        public IReadOnlyCollection<Address> Addresses => _addresses;

        public static Customer NewCustomer()
        {
            var customer = new Customer
            {
                _addresses = new List<Address>()
            };
            return customer;
        }

        public void AddAddress(Address address)
        {
            var existingAddress = _addresses.SingleOrDefault(s => s.Equals(address));

            if (existingAddress == null)
            {
                _addresses.Add(address);
            }
            else
            {
                throw new CustomerDomainException("Customer already exists!");
            }
        }
    }
}
