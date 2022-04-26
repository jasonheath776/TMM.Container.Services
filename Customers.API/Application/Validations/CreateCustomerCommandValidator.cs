namespace Customers.API.Application.Validations;

using Customers.API.Application.Commands;
using System.Net.Mail;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator(ILogger<CreateCustomerCommandValidator> logger)
    {
        RuleFor(command => command.EmailAddress).NotEmpty().Must(IsValidEmail).WithMessage("Must Be valid email address");
        RuleFor(command => command.Forename).NotEmpty();
        RuleFor(command => command.Surname).NotEmpty();
        RuleFor(command => command.MobileNo).NotEmpty();
        RuleFor(command => command.Title).NotEmpty();
        RuleFor(command => command.Addresses).NotEmpty().Must(ContainsAtLeastOneAddress).WithMessage("Must Be valid email address"); ;

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
