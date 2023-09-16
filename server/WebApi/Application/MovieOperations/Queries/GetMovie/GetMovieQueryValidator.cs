using FluentValidation;

namespace WebApi.Application.MovieOperations.Queries.GetMovie
{
    public class GetMovieQueryValidator : AbstractValidator<GetMovieQuery>
    {
        public GetMovieQueryValidator()
        {
            RuleFor(query => query.MovieId).GreaterThan(0);
        }
    }
}