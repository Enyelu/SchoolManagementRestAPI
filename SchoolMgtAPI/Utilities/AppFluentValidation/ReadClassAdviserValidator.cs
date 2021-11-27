using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Dtos;

namespace Utilities.AppFluentValidation
{
    public class ReadClassAdviserValidator : AbstractValidator<ReadClassAdviserDto>
    {
        public ReadClassAdviserValidator()
        {
            RuleFor(x => x.Level).NotEmpty().WithMessage("Field cannot be empty")
                                 .NotNull().WithMessage("Field cannot be empty")
                                 .GreaterThanOrEqualTo(1).WithMessage("Student level must be a digit and greater than zero(0)");
            
            RuleFor(x => x.Department).NotEmpty().WithMessage("Course name cannot be empty").NotNull().MinimumLength(5).WithMessage("Course title too small");
        }
    }
}
