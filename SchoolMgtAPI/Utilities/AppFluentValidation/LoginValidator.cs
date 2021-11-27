using FluentValidation;
using Utilities.Dtos;

namespace Utilities.AppFluentValidation
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).EmailAddress();

            RuleFor(x => x.Password).NotNull().WithMessage("Password is required")
                                              .NotEmpty()
                                              .MinimumLength(8).WithMessage("Password must contain at least 6 characters")
                                              .Matches("[A-Z]").WithMessage("Password must contain atleast 1 uppercase letter")
                                              .Matches("[a-z]").WithMessage("Password must contain atleast 1 lowercase letter")
                                              .Matches("[0-9]").WithMessage("Password must contain a number")
                                              .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain non alphanumeric");
        }
    }
}