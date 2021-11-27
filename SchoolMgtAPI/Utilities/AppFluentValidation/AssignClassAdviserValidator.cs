using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Dtos;

namespace Utilities.AppFluentValidation
{
    public class AssignClassAdviserValidator : AbstractValidator<AssignClassAdviserDto>
    {
        public AssignClassAdviserValidator()
        {
            RuleFor(x => x.LecturerEmail).EmailAddress();
            RuleFor(x => x.Level).NotEmpty().WithMessage("Field cannot be empty")
                                 .NotNull().WithMessage("Field cannot be empty")
                                 .GreaterThanOrEqualTo(1).WithMessage("Student level must be a digit and greater than zero(0)");
        }
    }
}
