using FluentValidation;

namespace ApiDemo.Models
{
    public class OrderDetailValidator:AbstractValidator<OrderDetailModel>
    {
        public OrderDetailValidator()
        {
            // Validation for OrderID
            RuleFor(o => o.OrderID)
                .GreaterThan(0).WithMessage("Order ID must be greater than 0.");

            // Validation for ProductID
            RuleFor(o => o.ProductID)
                .GreaterThan(0).WithMessage("Product ID must be greater than 0.");

            // Validation for Quantity (nullable)
            RuleFor(o => o.Quantity)
                .NotNull().WithMessage("Quantity must not be null.")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

            // Validation for Amount (nullable)
            RuleFor(o => o.Amount)
                .NotNull().WithMessage("Amount must not be null.")
                .GreaterThan(0).WithMessage("Amount must be greater than 0.");

            // Validation for TotalAmount (nullable)
            RuleFor(o => o.TotalAmount)
                .NotNull().WithMessage("Total Amount must not be null.")
                .GreaterThan(0).WithMessage("Total Amount must be greater than 0.");

            // Validation for UserID
            RuleFor(o => o.UserID)
                .GreaterThan(0).WithMessage("User ID must be greater than 0.");
        }
    }
}
