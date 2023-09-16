using FluentValidation;

namespace WebApi.Application.UserOperations.Queries.GetUser
{
    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(query => query.UserId).GreaterThan(0);
        }
    }
}