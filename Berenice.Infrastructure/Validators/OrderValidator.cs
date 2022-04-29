using Berenice.Core.Dtos;
using FluentValidation;

namespace Berenice.Infrastructure.Validators
{
    public class OrderValidator : AbstractValidator<OrderDTO>
    {
        public OrderValidator() 
        {
            RuleFor(order => order.OrderDate)
                .NotNull()
                .NotEmpty();

            RuleFor(order => order.RequiredDate)
                .NotNull()
                .NotEmpty();

            RuleFor(order => order.ShippedDate)
                .NotNull()
                .NotEmpty();
        }
    }
}
