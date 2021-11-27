using FluentValidation;
using Utilities.Dtos;

namespace Utilities.AppFluentValidation
{
    public class RegisterStudentValidator : AbstractValidator<RegisterStudentDto>
    {
        public RegisterStudentValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name field cannot be empty").NotNull().MinimumLength(3).WithMessage("Minimum name length is 2");
            RuleFor(x => x.MiddleName).NotEmpty().WithMessage("Middle name field cannot be empty").NotNull().MinimumLength(3).WithMessage("Minimum name length is 2"); ;
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name field cannot be empty").NotNull().MinimumLength(3).WithMessage("Minimum name length is 2"); ;
            RuleFor(x => x.PhoneNumber).Matches(@"^[0]\d{10}$").WithMessage("Phone number must start with 0 and must be 11 digits");
            RuleFor(x => x.Age).NotEmpty().InclusiveBetween(16, 50).WithMessage("Age must be between 16 and 50");

            RuleFor(x => x.Gender).NotEmpty().WithMessage("Field cannot be empty").NotNull().WithMessage("Field cannot be null");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Field cannot be empty").NotNull().WithMessage("Field cannot be null");
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotNull().WithMessage("Password is required")
                                              .NotEmpty()
                                              .MinimumLength(8).WithMessage("Password must contain at least 6 characters")
                                              .Matches("[A-Z]").WithMessage("Password must contain atleast 1 uppercase letter")
                                              .Matches("[a-z]").WithMessage("Password must contain atleast 1 lowercase letter")
                                              .Matches("[0-9]").WithMessage("Password must contain a number")
                                              .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain non alphanumeric");

            RuleFor(x => x.Class).NotEmpty().WithMessage("Field cannot be empty").NotNull().WithMessage("Field cannot be null");
            RuleFor(x => x.Level).NotEmpty().WithMessage("Field cannot be empty")
                                            .NotNull().WithMessage("Field cannot be null")
                                            .GreaterThanOrEqualTo(1).WithMessage("Student level must be greater than zero(0)");

            RuleFor(x => x.ClassAdviserEmail).EmailAddress();
            RuleFor(x => x.RegistrationNumber).NotEmpty().WithMessage("Field cannot be empty").NotNull().WithMessage("Field cannot be null");

            RuleFor(x => x.BirthDate).NotEmpty().WithMessage("Field cannot be empty").NotNull().WithMessage("Field cannot be null");
            RuleFor(x => x.DepartmentName).NotEmpty().WithMessage("Field cannot be empty").NotNull().WithMessage("Field cannot be null");
            RuleFor(x => x.FacultyName).NotEmpty().WithMessage("Field cannot be empty").NotNull().WithMessage("Field cannot be null");
            RuleFor(x => x.StreetNumber).NotEmpty().WithMessage("Field cannot be empty").NotNull().WithMessage("Field cannot be null");
            RuleFor(x => x.City).NotEmpty().WithMessage("Field cannot be empty").NotNull().WithMessage("Field cannot be null");
            RuleFor(x => x.State).NotEmpty().WithMessage("Field cannot be empty").NotNull().WithMessage("Field cannot be null");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Field cannot be empty").NotNull().WithMessage("Field cannot be null");
        }
    }
}