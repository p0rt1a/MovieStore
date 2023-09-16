using FluentValidation;

namespace WebApi.Application.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(command => command.UserId).GreaterThan(0);
        }
    }
}