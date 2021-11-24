using FluentValidation;
using Utilities.Dtos;

namespace Utilities.AppFluentValidation
{
    public class NonAcademicStaffUpdateValidator : AbstractValidator<NonAcademicStaffUpdateDto>
    {
        public NonAcademicStaffUpdateValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name field cannot be empty").NotNull().MinimumLength(3).WithMessage("Minimum name length is 2");
            RuleFor(x => x.MiddleName).NotEmpty().WithMessage("Middle name field cannot be empty").NotNull().MinimumLength(3).WithMessage("Minimum name length is 2"); ;
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name field cannot be empty").NotNull().MinimumLength(3).WithMessage("Minimum name length is 2"); ;
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().MinimumLength(11).WithMessage("Invalid number");
            RuleFor(x => x.StreetNumber).NotEmpty().WithMessage("Field cannot be null").NotNull().WithMessage("Field cannot be null");
            RuleFor(x => x.City).NotEmpty().WithMessage("Field cannot be null").NotNull().WithMessage("Field cannot be null");
            RuleFor(x => x.State).NotEmpty().WithMessage("Field cannot be null").NotNull().WithMessage("Field cannot be null");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Field cannot be null").NotNull().WithMessage("Field cannot be null");
        }
    }
}