using FluentValidation;

namespace ApiDemo.Models
{
    public class ProductValidator:AbstractValidator<ProductModel>
    {
        public ProductValidator()
        {
            // Validation for ProductName
            RuleFor(p => p.ProductName)
                .NotNull().WithMessage("Product name is required.")
                .NotEmpty().WithMessage("Product name cannot be empty.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            // Validation for ProductPrice
            RuleFor(p => p.ProductPrice)
                .NotNull().WithMessage("Product price is required.")
                .GreaterThan(0).WithMessage("Product price must be greater than 0.");

            // Validation for ProductCode
            RuleFor(p => p.ProductCode)
                .NotNull().WithMessage("Product code is required.")
                .NotEmpty().WithMessage("Product code cannot be empty.")
                .Matches(@"^[A-Za-z0-9]{5,20}$").WithMessage("Product code must be alphanumeric and between 5 to 20 characters.");

            // Validation for Description
            RuleFor(p => p.Description)
                .NotNull().WithMessage("Description is required.")
                .NotEmpty().WithMessage("Description cannot be empty.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            // Validation for UserID
            RuleFor(p => p.UserID)
                .NotNull().WithMessage("User ID is required.")
                .GreaterThan(0).WithMessage("User ID must be greater than 0.");
        }
    }
}
