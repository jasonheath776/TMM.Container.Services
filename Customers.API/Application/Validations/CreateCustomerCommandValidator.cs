namespace Customers.API.Application.Validations;

using Customers.API.Application.Commands;
using System.Net.Mail;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator(ILogger<CreateCustomerCommandValidator> logger)
    {
        RuleFor(command => command.EmailAddress).NotEmpty().Must(IsValidEmail).WithMessage("Email adddress: Is invalid!");
        RuleFor(command => command.Forename).NotEmpty().WithMessage("Forename: Should not be empty!");
        RuleFor(command => command.Surname).NotEmpty().WithMessage("Surname: Should not be empty!"); ;
        RuleFor(command => command.MobileNo).NotEmpty().WithMessage("Forename: Should not be empty!"); 
        RuleFor(command => command.Title).NotEmpty().WithMessage("Title: Should not be empty!"); ;
        RuleFor(command => command.Addresses).NotEmpty().Must(ContainsAtLeastOneAddress).WithMessage("Addresses: At least one address is needed!"); ;

        logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
    }

    private bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false; 
        }
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }

    private bool ContainsAtLeastOneAddress(IEnumerable<Address> addresses)
    {
        return addresses.Any();
    }
}
