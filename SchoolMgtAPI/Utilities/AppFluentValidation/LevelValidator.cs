using FluentValidation;
using Utilities.Dtos;

namespace Utilities.AppFluentValidation
{
    public class LevelValidator : AbstractValidator<LevelDto>
    {
        public LevelValidator()
        {
            RuleFor(x => x.Level).NotEmpty().WithMessage("Field cannot be empty")
                                 .NotNull().WithMessage("Field cannot be empty")
                                 .GreaterThanOrEqualTo(1).WithMessage("Student level must be a digit and greater than zero(0)");
        }
    }
}
