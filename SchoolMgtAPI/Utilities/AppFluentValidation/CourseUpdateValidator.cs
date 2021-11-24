using FluentValidation;
using Utilities.Dtos;

namespace Utilities.AppFluentValidation
{
    public class CourseUpdateValidator : AbstractValidator<CourseUpdateDto>
    {
        public CourseUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Course name cannot be empty").NotNull().MinimumLength(5).WithMessage("Course title too small");
            RuleFor(x => x.CourseCode).NotEmpty().WithMessage("CourseCode cannot be empty").NotNull().MinimumLength(6).MaximumLength(6);
            RuleFor(x => x.CourseUnit).GreaterThanOrEqualTo(1).WithMessage("Course unit must be greater than zero(0)").NotEmpty();
            RuleFor(x => x.NewCourseCode).NotEmpty().WithMessage("CourseCode cannot be empty").NotNull().MinimumLength(6).MaximumLength(6);
        }
    }
}
