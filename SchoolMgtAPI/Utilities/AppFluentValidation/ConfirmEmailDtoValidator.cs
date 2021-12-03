using FluentValidation;
using Utilities.Dtos;

namespace Utilities.AppFluentValidation
{
    public class ConfirmEmailDtoValidator : AbstractValidator<ConfirmEmailDto>
    {
        public ConfirmEmailDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("Field cannot be empty");
            RuleFor(x => x.Token).NotEmpty().WithMessage("Field cannot be empty").Null().WithMessage("Field cannot be null");
        }
    }
}
