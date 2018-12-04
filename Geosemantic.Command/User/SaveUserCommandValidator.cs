using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Geosemantic.Data;
using Microsoft.EntityFrameworkCore;

namespace Geosemantic.Command.User
{
    public class SaveUserCommandValidator : AbstractValidator<SaveUserCommand>
    {
        private readonly GeosemanticEntities context;

        public SaveUserCommandValidator(GeosemanticEntities context)
        {
            this.context = context;

            RuleFor(i => i.EmailAddress)
                .NotEmpty().WithMessage("Email Address is required")
                .EmailAddress().WithMessage("Invalid Email Address")
                .Length(0, 50).WithMessage("Email Address should not exceed 50 characters")
                .MustAsync(IsEmailAddressAlreadyRegistered).WithMessage("Email Address already registered").When(i => i.Id == 0); ;

            RuleFor(i => i.Password)
                .NotEmpty().WithMessage("Password is required")
                .Equal(customer => customer.ConfirmPassword).WithMessage("Passwords do not match");

            RuleFor(i => i.ConfirmPassword)
                .NotEmpty().WithMessage("Password is required");

            RuleFor(i => i.FirstName)
                .NotEmpty().WithMessage("First Name is required")
                .Length(0, 50).WithMessage("First Name should not exceed 50 characters");

            RuleFor(i => i.LastName)
                .NotEmpty().WithMessage("Last Name is required")
                .Length(0, 50).WithMessage("Last Name should not exceed 50 characters");


            RuleFor(i => i.MobileNumber)
                .NotEmpty().WithMessage("MobileNumber is required")
                .Length(10, 10).WithMessage("Mobile Number should have 10 characters");

            RuleFor(i => i.DateOfBirth)
                .NotEmpty().WithMessage("Date Of Birth is required");

            RuleFor(i => i.GenderType)
                .NotEmpty().WithMessage("Gender is required");

            RuleFor(i => i.RoleId)
                .NotEmpty().WithMessage("Role is required");
        }

        private async Task<bool> IsEmailAddressAlreadyRegistered(string emailAddress, CancellationToken cancellationToken)
        {
            var result = await context.User
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.EmailAddress.Equals(emailAddress), cancellationToken);

            return result == null;
        }
    }
}
