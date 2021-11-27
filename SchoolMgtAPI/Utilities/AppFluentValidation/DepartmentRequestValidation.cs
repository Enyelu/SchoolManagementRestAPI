using FluentValidation;
using Utilities.Dtos;

namespace Utilities.AppFluentValidation
{
    public class DepartmentRequestValidation : AbstractValidator<DepartmentRequestDto>
    {
        public DepartmentRequestValidation()
        {
            RuleFor(x => x.DepartmentName).NotEmpty().WithMessage("Field cannot be empty")
                                          .NotNull().WithMessage("Field cannot be null")
                                          .MinimumLength(5).WithMessage("Department name must have atleast 5 letters");
           
            RuleFor(x => x.FacultyName).NotEmpty().WithMessage("Field cannot be empty")
                                       .NotNull().WithMessage("Field cannot be null")
                                       .MinimumLength(5).WithMessage("Faculty name must have atleast 5 letters");
        }
    }
   
}
