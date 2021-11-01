
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Dtos;

namespace Utilities.AppFluentValidation
{
    public class AddressValidator : AbstractValidator<AddressValidationDto>
    {
        public AddressValidator()
        {

        }
    }
}
