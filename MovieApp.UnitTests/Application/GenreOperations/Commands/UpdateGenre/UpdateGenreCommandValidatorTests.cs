using AutoMapper;
using FluentAssertions;
using MovieApp.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DbOperations;
using Xunit;

namespace MovieApp.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateGenreCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidGenreIdIsGiven_Validator_ShouldBeReturnError()
        {
            UpdateGenreCommand command = new(null, null);
            command.GenreId = 0;

            UpdateGenreCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidGenreIdIsGiven_Validator_ShouldNotBeReturnError()
        {
            UpdateGenreCommand command = new(null, null);
            command.GenreId = 1;

            UpdateGenreCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }
}
