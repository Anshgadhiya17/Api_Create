using FluentValidation;

namespace ApiDemo.Models
{
    public class OrderValidator:AbstractValidator<OrderModel>
    {
        public OrderValidator()
        {
            // Validation for OrderDate
            RuleFor(o => o.OrderDate)
                .NotNull().WithMessage("Order date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Order date cannot be in the future.");

            // Validation for CustomerID
            RuleFor(o => o.CustomerID)
                .GreaterThan(0).WithMessage("Customer ID must be greater than 0.");

            // Validation for PaymentMode
            RuleFor(o => o.PaymentMode)
                .NotNull().WithMessage("Payment mode is required.")
                .NotEmpty().WithMessage("Payment mode cannot be empty.")
                .MaximumLength(50).WithMessage("Payment mode cannot exceed 50 characters.");

            // Validation for TotalAmount
            RuleFor(o => o.TotalAmount)
                .NotNull().WithMessage("Total amount is required.")
                .GreaterThan(0).WithMessage("Total amount must be greater than 0.");

            // Validation for ShippingAddress
            RuleFor(o => o.ShippingAddress)
                .NotNull().WithMessage("Shipping address is required.")
                .NotEmpty().WithMessage("Shipping address cannot be empty.")
                .MaximumLength(200).WithMessage("Shipping address cannot exceed 200 characters.");

            // Validation for UserID
            RuleFor(o => o.UserID)
                .GreaterThan(0).WithMessage("User ID must be greater than 0.");
        }
    }
}
