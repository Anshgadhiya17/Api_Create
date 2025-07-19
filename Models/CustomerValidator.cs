using FluentValidation;

namespace ApiDemo.Models
{
    public class CustomerValidator : AbstractValidator<CustomerModel>
    {
        public CustomerValidator()
        {
            // Validation for CustomerName
            RuleFor(c => c.CustomerName)
                .NotNull().WithMessage("Customer name must not be null.")
                .NotEmpty().WithMessage("Customer name is required.")
                .MaximumLength(100).WithMessage("Customer name must not exceed 100 characters.");

            // Validation for HomeAddress
            RuleFor(c => c.HomeAddress)
                .NotNull().WithMessage("Home address must not be null.")
                .NotEmpty().WithMessage("Home address is required.")
                .MaximumLength(200).WithMessage("Home address must not exceed 200 characters.");

            // Validation for Email
            RuleFor(c => c.Email)
                .NotNull().WithMessage("Email must not be null.")
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            // Validation for MobileNo
            RuleFor(c => c.MobileNo)
                .NotNull().WithMessage("Mobile number must not be null.")
                .NotEmpty().WithMessage("Mobile number is required.")
                .Matches(@"^\d{10}$").WithMessage("Mobile number must be exactly 10 digits.");

            // Validation for Gst_No
            RuleFor(c => c.Gst_No)
                .NotNull().WithMessage("GST number must not be null.")
                .NotEmpty().WithMessage("GST number is required.")
                .Matches(@"^[A-Z0-9]{15}$").WithMessage("GST number must consist of 15 uppercase alphanumeric characters.");


            // Validation for CityName
            RuleFor(c => c.CityName)
                .NotNull().WithMessage("City name must not be null.")
                .NotEmpty().WithMessage("City name is required.")
                .MaximumLength(100).WithMessage("City name must not exceed 100 characters.");

            // Validation for PinCode
            RuleFor(c => c.PinCode)
                .NotNull().WithMessage("Pin code must not be null.")
                .NotEmpty().WithMessage("Pin code is required.")
                .Matches(@"^\d{6}$").WithMessage("Pin code must be exactly 6 digits.");

            // Validation for NetAmount
            RuleFor(c => c.NetAmount)
                .NotNull().WithMessage("Net amount must not be null.")
                .GreaterThan(0).WithMessage("Net amount must be greater than 0.");

            // Validation for UserID
            RuleFor(c => c.UserID)
                .NotNull().WithMessage("User ID must not be null.")
                .GreaterThan(0).WithMessage("User ID must be greater than 0.");
        }
    }
}
