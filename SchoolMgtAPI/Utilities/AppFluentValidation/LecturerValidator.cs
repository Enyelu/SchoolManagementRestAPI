﻿
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Dtos;

namespace Utilities.AppFluentValidation
{
    public class LecturerValidator : AbstractValidator<LecturerValidationDto>
    {
        public LecturerValidator()
        {

        }
    }
}