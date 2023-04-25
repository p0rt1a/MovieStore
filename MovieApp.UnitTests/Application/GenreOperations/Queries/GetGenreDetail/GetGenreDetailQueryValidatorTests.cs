using AutoMapper;
using FluentAssertions;
using MovieApp.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DbOperations;
using Xunit;

namespace MovieApp.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidGenreIdIsGiven_Validator_ShouldBeReturnError()
        {
            GetGenreDetailQuery query = new(null, null);
            query.GenreId = 0;

            GetGenreDetailQueryValidator validator = new();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidGenreIdIsGiven_Validator_ShouldNotBeReturnError()
        {
            GetGenreDetailQuery query = new(null, null);
            query.GenreId = 1;

            GetGenreDetailQueryValidator validator = new();
            var result = validator.Validate(query);

            result.Errors.Count.Should().Equals(0);
        }
    }
}
