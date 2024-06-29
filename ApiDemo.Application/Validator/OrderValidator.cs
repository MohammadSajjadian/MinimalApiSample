using ApiDemo.Domain.Entities;
using FluentValidation;

namespace ApiDemo.Application.Validator
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x.Date).NotEmpty().WithMessage("Date should not be empty.");
        }
    }
}
