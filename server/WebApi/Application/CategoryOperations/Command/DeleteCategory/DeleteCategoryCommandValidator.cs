using FluentValidation;

namespace WebApi.Application.CategoryOperations.Command.DeleteCategory
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(command => command.CategoryId).GreaterThan(0);
        }
    }
}