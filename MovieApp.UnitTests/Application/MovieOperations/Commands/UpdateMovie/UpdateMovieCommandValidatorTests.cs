using AutoMapper;
using FluentAssertions;
using MovieApp.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.MovieOperations.Commands.UpdateMovie;
using WebApi.DbOperations;
using Xunit;

namespace MovieApp.UnitTests.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateMovieCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidMovieIdIsGiven_Validator_ShouldBeReturnError()
        {
            UpdateMovieCommand command = new(null);
            command.MovieId = 0;

            UpdateMovieCommandValidator validator = new();

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            UpdateMovieCommand command = new(null);
            command.MovieId = 1;

            UpdateMovieCommandValidator validator = new();

            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }
}
