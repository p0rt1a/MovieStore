using FluentValidation;
using WebApi.DbOperations;

namespace WebApi.Application.CategoryOperations.Command.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(command => command.CategoryId).GreaterThan(0);
        }
    }
}