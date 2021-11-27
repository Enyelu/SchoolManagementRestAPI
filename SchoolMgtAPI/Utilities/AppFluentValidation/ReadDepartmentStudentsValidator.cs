using FluentValidation;
using Utilities.Dtos;

namespace Utilities.AppFluentValidation
{
    public class ReadDepartmentStudentsValidator : AbstractValidator<ReadDepartmentsStudentDto>
    {
        public ReadDepartmentStudentsValidator()
        {
            RuleFor(x => x.DepartmentName).NotEmpty().WithMessage("Field cannot be empty")
                                          .NotNull().WithMessage("Field cannot be null")
                                          .MinimumLength(5).WithMessage("Faculty name must have atleast 5 letters");

            RuleFor(x => x.Level).NotEmpty().WithMessage("Field cannot be empty")
                                 .NotNull().WithMessage("Field cannot be null")
                                 .GreaterThanOrEqualTo(1).WithMessage("Student level must be greater than zero(0)");
        }
    }
}
