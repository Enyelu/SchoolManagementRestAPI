using FluentValidation;
using Utilities.Dtos;

namespace Utilities.AppFluentValidation
{
    public class UpdateAddressValidator : AbstractValidator<UpdateAddressDto>
    {
        public UpdateAddressValidator()
        {
            RuleFor(x => x.StreetNumber).NotEmpty().WithMessage("Field cannot be empty").NotNull().WithMessage("Field cannot be null");
            RuleFor(x => x.City).NotEmpty().WithMessage("Field cannot be empty").NotNull().WithMessage("Field cannot be null");
            RuleFor(x => x.State).NotEmpty().WithMessage("Field cannot be empty").NotNull().WithMessage("Field cannot be null");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Field cannot be empty").NotNull().WithMessage("Field cannot be null");
        }
    }
}
