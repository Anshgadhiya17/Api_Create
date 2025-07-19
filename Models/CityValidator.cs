using FluentValidation;

namespace ApiDemo.Models
{
    public class CityValidator : AbstractValidator<CityModel>
    {
        public CityValidator()
        {
            // Validation for CityName
            RuleFor(c => c.CityName)
                .NotNull().WithMessage("City name must not be null.")
                .NotEmpty().WithMessage("City name is required.")
                .MaximumLength(20).WithMessage("City name must not exceed 20 characters.")
                .NotEqual("string").WithMessage("City name cannot have the default value 'string'.");

            // Validation for CityCode
            RuleFor(c => c.CityCode)
                .NotNull().WithMessage("City code must not be null.")
                .NotEmpty().WithMessage("City code is required.")
                .MinimumLength(2).WithMessage("City code must be at least 2 characters long.")
                .NotEqual("string").WithMessage("City code cannot have the default value 'string'.");
        }
    }
}
