using FluentValidation;

namespace WebApi.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.Model.Name.Trim()).NotEmpty().MinimumLength(2);
            RuleFor(command => command.Model.Surname.Trim()).NotEmpty().MinimumLength(2);
            RuleFor(command => command.Model.Email.Trim()).NotEmpty();
            RuleFor(command => command.Model.Password.Trim()).NotEmpty().MinimumLength(7);
        }
    }
}