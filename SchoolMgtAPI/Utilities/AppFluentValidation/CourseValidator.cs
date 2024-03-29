﻿using FluentValidation;
using Utilities.Dtos;

namespace Utilities.AppFluentValidation
{
    public class CourseValidator : AbstractValidator<CourseDto>
    {
        public CourseValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Course name cannot be empty").NotNull().MinimumLength(5).WithMessage("Course title too small");
            RuleFor(x => x.CourseCode).NotEmpty().WithMessage("CourseCode cannot be empty").NotNull().MinimumLength(6).MaximumLength(6);
            RuleFor(x => x.CourseUnit).GreaterThanOrEqualTo(1).WithMessage("Course unit must be greater than zero(0)").NotEmpty();
            RuleFor(x => x.DepartmentName).NotEmpty().WithMessage("Field cannot be empty").NotNull().MinimumLength(5).WithMessage("Department name must exceed 4 letters");
            RuleFor(x => x.FacultyName).NotEmpty().WithMessage("Field cannot be empty").NotNull().MinimumLength(5).WithMessage("Faculty name must exceed 4 letters");
        }
    }
}