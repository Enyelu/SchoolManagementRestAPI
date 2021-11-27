using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Dtos;

namespace Utilities.AppFluentValidation
{
    public  class AssignCourseValidator :AbstractValidator<AssignCourseDto>
    {
        public AssignCourseValidator()
        {
            RuleFor(x => x.LecturerEmail).EmailAddress();
        }
    }
}
