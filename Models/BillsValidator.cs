using FluentValidation;

namespace ApiDemo.Models
{
    public class BillsValidator:AbstractValidator<BillsModel>
    {
        public BillsValidator()
        {
            // Validation for BillNumber
            RuleFor(b => b.BillNumber)
                .NotNull().WithMessage("Bill number must not be null.")
                .NotEmpty().WithMessage("Bill number is required.")
                .MaximumLength(50).WithMessage("Bill number must not exceed 50 characters.");

            // Validation for BillDate
            RuleFor(b => b.BillDate)
                .NotNull().WithMessage("Bill date must not be null.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Bill date cannot be in the future.");

            // Validation for OrderID
            RuleFor(b => b.OrderID)
                .GreaterThan(0).WithMessage("Order ID must be greater than 0.");

            // Validation for TotalAmount
            RuleFor(b => b.TotalAmount)
                .NotNull().WithMessage("Total amount must not be null.")
                .GreaterThan(0).WithMessage("Total amount must be greater than 0.");

            // Validation for Discount
            RuleFor(b => b.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("Discount cannot be negative.")
                .LessThanOrEqualTo(b => b.TotalAmount ?? 0).WithMessage("Discount cannot exceed total amount.");

            // Validation for NetAmount
            RuleFor(b => b.NetAmount)
                .NotNull().WithMessage("Net amount must not be null.")
                .Equal(b => (b.TotalAmount ?? 0) - (b.Discount ?? 0)).WithMessage("Net amount must be equal to total amount minus discount.");
        }
    }
}
