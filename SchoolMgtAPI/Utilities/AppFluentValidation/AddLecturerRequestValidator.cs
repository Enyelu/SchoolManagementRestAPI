using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Dtos;

namespace Utilities.AppFluentValidation
{
    public class AddLecturerRequestValidator : AbstractValidator<AddLecturerRequestDto>
    {
        public AddLecturerRequestValidator()
        {
            RuleFor(x => x.DepartmentName).NotEmpty().WithMessage("Field cannot be empty")
                                         .NotNull().WithMessage("Field cannot be null")
                                         .MinimumLength(5).WithMessage("Department name must have atleast 5 letters");
            RuleFor(x => x.LecturerEmail).EmailAddress();
        }
    }
}
