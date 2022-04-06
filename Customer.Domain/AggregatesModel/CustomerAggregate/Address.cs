#nullable disable

namespace Customers.Domain.AggregatesModel.CustomerAggregate
{
    public class Address : ValueObject
    {
        private const string UK = "UK";

        public int AddressId { get; set; }

        //[Required]
        //[StringLength(80, ErrorMessage = "Address Line 1 length can't be more than 80.")]
        public string AddressLine1 { get; set; }

        //[StringLength(80, ErrorMessage = "Address Line 2 length can't be more than 80.")]
        public string AddressLine2 { get; set; }

        //[Required]
        //[StringLength(50, ErrorMessage = "Town length can't be more than 50.")]
        public string Town { get; set; }


        //[StringLength(50, ErrorMessage = "County length can't be more than 50.")]
        public string County { get; set; }

        //[Required]
        //[StringLength(10, ErrorMessage = "Post Code length can't be more than 10.")]
        public string PostCode { get; set; }

        //[StringLength(30, ErrorMessage = "Country length can't be more than 30.")]
        public string Country { get; set; } = UK;

        public bool MainAddress { get; set; }

        public int CustomerId { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AddressLine1;
            yield return AddressLine2;
            yield return Town;
            yield return County;
            yield return Country;
            yield return PostCode;
        }
    }
}
