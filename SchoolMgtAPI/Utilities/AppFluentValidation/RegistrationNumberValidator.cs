using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Dtos;

namespace Utilities.AppFluentValidation
{
    public class RegistrationNumberValidator : AbstractValidator<RegistrationNumberDto>
    {
        public RegistrationNumberValidator()
        {
            RuleFor(x => x.RegistrationNumber).NotEmpty().WithMessage("Field cannot be empty")
                                              .NotNull().WithMessage("Field cannot be null")
                                              .MinimumLength(11).WithMessage("Registration number must be 11 digits")
                                              .MaximumLength(11).WithMessage("Registration number must not exceed 11 digits");
        }
    }
}
