using Berenice.Core.Dtos;
using FluentValidation;

namespace Berenice.Infrastructure.Validators
{
    public  class CustomerValidator : AbstractValidator<CustomerDTO>
    {
        public CustomerValidator() 
        {
            RuleFor(customer => customer.FirstName)
                .NotEmpty()
                .Length(50)
                .NotNull();

            RuleFor(customer => customer.LastName)
                .NotEmpty()
                .Length(50)
                .NotNull();

            RuleFor(customer => customer.Email)
                .EmailAddress()
                .Length(100)
                .NotEmpty()
                .NotNull();

            RuleFor(customer => customer.Phone)
                .NotEmpty()
                .Length(10)
                .NotNull();

            RuleFor(customer => customer.State)
                .NotEmpty()
                .Length(50)
                .NotNull();

            RuleFor(customer => customer.Street)
                .NotEmpty()
                .Length(100)
                .NotNull();

            RuleFor(customer => customer.ZipCode)
                .NotEmpty()
                .Length(5)
                .NotNull();

        }
    }
}
