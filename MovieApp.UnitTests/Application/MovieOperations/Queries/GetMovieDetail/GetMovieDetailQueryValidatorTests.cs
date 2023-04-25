using AutoMapper;
using FluentAssertions;
using MovieApp.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using WebApi.DbOperations;
using Xunit;

namespace MovieApp.UnitTests.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetMovieDetailQueryValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidMovieIdIsGiven_Validator_ShouldBeReturnError()
        {
            GetMovieDetailQuery query = new(null, null);
            query.MovieId = 0;

            GetMovieDetailQueryValidator validator = new();

            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidMovieIdIsGiven_Validator_ShouldNotBeReturnError()
        {
            GetMovieDetailQuery query = new(null, null);
            query.MovieId = 1;

            GetMovieDetailQueryValidator validator = new();

            var result = validator.Validate(query);

            result.Errors.Count.Should().Equals(0);
        }
    }
}
