using FluentValidation;
using Utilities.Dtos;

namespace Utilities.AppFluentValidation
{
    public class RegistrationNumberValidator : AbstractValidator<RegistrationNumberDto>
    {
        public RegistrationNumberValidator()
        {
            RuleFor(x => x.RegistrationNumber).MinimumLength(11).WithMessage("Registration number must be 11 digits");

        }
    }
}
