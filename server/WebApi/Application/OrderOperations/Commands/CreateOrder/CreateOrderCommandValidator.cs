using FluentValidation;

namespace WebApi.Application.OrderOperations.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(command => command.Model.UserId).GreaterThan(0);
            RuleFor(command => command.Model.MovieId).GreaterThan(0);
            RuleFor(command => command.Model.Price).NotEmpty().GreaterThan(0);
        }
    }
}