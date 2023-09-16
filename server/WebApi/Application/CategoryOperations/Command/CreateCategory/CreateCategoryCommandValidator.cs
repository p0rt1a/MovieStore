using FluentValidation;

namespace WebApi.Application.CategoryOperations.Command.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(command => command.Model.Name.Trim()).NotEmpty().MinimumLength(2);
        }
    }
}