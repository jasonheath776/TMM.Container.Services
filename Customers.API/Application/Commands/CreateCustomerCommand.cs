#nullable disable

namespace Customers.API.Application.Commands
{
    public class CreateCustomerCommand : IRequest<Customer>
    {
        [DataMember]
        private readonly List<Address> _addresses;

        [DataMember]
        public int CustomerId { get; private set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Forename { get; set; }

        [DataMember]
        public string Surname { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public string MobileNo { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public IEnumerable<Address> Addresses => _addresses;

        public CreateCustomerCommand()
        {
            _addresses = new List<Address>();
        }

        public CreateCustomerCommand(List<Address> addresses, string title, string forname, string surname, string emailAddress, string mobileNo, bool isActive) : this()
        {
            _addresses = addresses;
            Title = title;
            Forename = forname;
            Surname = surname;
            EmailAddress = emailAddress;
            MobileNo = mobileNo;
            IsActive = isActive;

        }
    }
}
