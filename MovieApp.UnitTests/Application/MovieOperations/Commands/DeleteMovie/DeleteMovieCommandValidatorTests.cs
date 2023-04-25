using AutoMapper;
using FluentAssertions;
using MovieApp.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.MovieOperations.Commands.DeleteMovie;
using WebApi.DbOperations;
using Xunit;

namespace MovieApp.UnitTests.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteMovieCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidMovieIdIsGiven_Validator_ShouldBeReturnError()
        {
            DeleteMovieCommand command = new(null);
            command.MovieId = 0;

            DeleteMovieCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidMovieIdIsGiven_Validator_ShoulNotBeReturnError()
        {
            DeleteMovieCommand command = new(null);
            command.MovieId = 1;

            DeleteMovieCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }
}
