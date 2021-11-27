using FluentValidation;
using Utilities.Dtos;

namespace Utilities.AppFluentValidation
{
    public class NameValidator : AbstractValidator<NameDto>
    {
        public NameValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Field cannot be empty")
                                .NotNull().WithMessage("Field cannot be null")
                                .MinimumLength(5).WithMessage("Name name must have atleast 5 letters");
        }
    }
}
