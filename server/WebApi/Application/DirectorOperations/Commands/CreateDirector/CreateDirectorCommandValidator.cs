using System;
using FluentValidation;

namespace WebApi.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator()
        {
            RuleFor(command => command.Model.FullName.Trim()).NotEmpty().MinimumLength(5);
            RuleFor(command => command.Model.DateOfBirth.Date).LessThan(DateTime.Now.Date.AddYears(-8));
            RuleFor(command => command.Model.ImageUrl.Trim()).NotEmpty();
        }
    }
}