using System;
using FluentValidation;

namespace WebApi.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator()
        {
            RuleFor(command => command.Model.Title.Trim()).NotEmpty().MinimumLength(1);
            RuleFor(command => command.Model.ImageUrl.Trim()).NotEmpty().MinimumLength(10);
            RuleFor(command => command.Model.CategoryId).GreaterThan(0);
            RuleFor(command => command.Model.DirectorId).GreaterThan(0);
            RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThanOrEqualTo(DateTime.Now.Date);
            RuleFor(command => command.Model.Price).GreaterThan(0);
        }
    }
}