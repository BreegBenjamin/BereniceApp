using Berenice.Core.Dtos;
using FluentValidation;

namespace Berenice.Infrastructure.Validators
{
    public class ProductValidator : AbstractValidator<ProductDTO>
    {
        public ProductValidator() 
        {
            RuleFor(product => product.Price)
                .NotNull();

            RuleFor(product => product.Brand)
                .NotNull()
                .NotEmpty()
                .Length(50);

            RuleFor(product => product.Category)
                .NotNull()
                .NotEmpty()
                .Length(50);

            RuleFor(product => product.ProductName)
                .NotNull()
                .NotEmpty()
                .Length(100);
        }
    }
}
