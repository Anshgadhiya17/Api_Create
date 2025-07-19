using FluentValidation;

namespace ApiDemo.Models
{
    public class UserValidator:AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            // Validation for UserName
            RuleFor(u => u.UserName)
                .NotNull().WithMessage("User name is required.")
                .NotEmpty().WithMessage("User name cannot be empty.")
                .MaximumLength(50).WithMessage("User name cannot exceed 50 characters.");

            // Validation for Email
            RuleFor(u => u.Email)
                .NotNull().WithMessage("Email is required.")
                .NotEmpty().WithMessage("Email cannot be empty.")
                .EmailAddress().WithMessage("Invalid email format.");

            // Validation for Password
            RuleFor(u => u.Password)
                .NotNull().WithMessage("Password is required.")
                .NotEmpty().WithMessage("Password cannot be empty.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{6,}$").WithMessage("Password must contain at least one letter and one number.");

            // Validation for MobileNo
            RuleFor(u => u.MobileNo)
                .NotNull().WithMessage("Mobile number is required.")
                .NotEmpty().WithMessage("Mobile number cannot be empty.")
                .Matches(@"^\d{10}$").WithMessage("Mobile number must be exactly 10 digits.");

            // Validation for Address
            RuleFor(u => u.Address)
                .NotNull().WithMessage("Address is required.")
                .NotEmpty().WithMessage("Address cannot be empty.")
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters.");

            // Validation for IsActive
            RuleFor(u => u.IsActive)
                .NotNull().WithMessage("IsActive must not be null.");
        }
    }
}
