using FluentValidation;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.AppFluentValidation
{
    public class ResetPasswordModelValidator : AbstractValidator<ResetPasswordModel>
    {
        public ResetPasswordModelValidator()
        {
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Field cannot be empty").NotNull().WithMessage("Field cannot be null");
            RuleFor(x => x.ConfirmNewPassword).NotEmpty().WithMessage("Field cannot be empty").NotNull().WithMessage("Field cannot be null");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Field cannot be empty").NotNull().WithMessage("Field cannot be null");
            RuleFor(x => x.Token).NotEmpty().WithMessage("Field cannot be empty").NotNull().WithMessage("Field cannot be null");
        }
    }
}
