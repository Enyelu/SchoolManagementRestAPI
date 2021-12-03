using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Dtos;

namespace Utilities.AppFluentValidation
{
    public class RefreshTokenRequestDtoValidator : AbstractValidator<RefreshTokenRequestDto>
    {
        public RefreshTokenRequestDtoValidator()
        {
            RuleFor(x => x.RefreshToken).NotEmpty().WithMessage("Field cannot be empty").NotNull().WithMessage("Field cannot be null");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Field cannot be empty").NotNull().WithMessage("Field cannot be null");
        }
    }
}
