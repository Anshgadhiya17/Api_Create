using FluentValidation;

namespace ApiDemo.Models
{
    public class StateValidator:AbstractValidator<StateModel>
    {
        public StateValidator()
        {
            // Validation for StateName
            RuleFor(s => s.StateName)
                .NotNull().WithMessage("State name must not be null.")
                .NotEmpty().WithMessage("State name is required.")
                .MaximumLength(20).WithMessage("State name must not exceed 20 characters.")
                .NotEqual("string").WithMessage("State name cannot have the default value 'string'.");

            // Validation for StateCode
            RuleFor(s => s.StateCode)
                .NotNull().WithMessage("State code must not be null.")
                .NotEmpty().WithMessage("State code is required.")
                .MinimumLength(2).WithMessage("State code must be at least 2 characters long.")
                .NotEqual("string").WithMessage("State code cannot have the default value 'string'.");
        }
    }
}
