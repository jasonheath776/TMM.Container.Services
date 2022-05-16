#nullable disable
namespace Customers.Domain.Exceptions
{
    public class CustomerDomainException : Exception
    {
        public IEnumerable<string> Errors { get; set; }

        public CustomerDomainException()
        { }

        public CustomerDomainException(string message)
            : base(message)
        { }

        public CustomerDomainException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public CustomerDomainException(string message, Exception innerException, List<string> validationFailures)
           : base(message, innerException)
        {
            Errors = validationFailures;
        }
    }

}
