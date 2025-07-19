using FluentValidation;

namespace ApiDemo.Models
{
    public class CountryValidator : AbstractValidator<CountryModel>
    {
        public CountryValidator() {
            // Validation for CountryName
            RuleFor(c => c.CountryName)
                .NotNull().WithMessage("Country name must not be null.")
                .NotEmpty().WithMessage("Country name is required.")
                .NotEqual("string").WithMessage("Country name cannot have the default value 'string'.")
                .MaximumLength(20).WithMessage("Country name must not exceed 20 characters.");

            // Validation for CountryCode
            RuleFor(c => c.CountryCode)
                .NotNull().WithMessage("Country code must not be null.")
                .NotEmpty().WithMessage("Country code is required.")
                .NotEqual("string").WithMessage("Country code cannot have the default value 'string'.")
                .MinimumLength(2).WithMessage("Country code must be at least 2 characters long.");
        }
    }
}
