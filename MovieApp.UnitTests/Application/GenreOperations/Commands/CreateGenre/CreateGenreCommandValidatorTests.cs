using AutoMapper;
using FluentAssertions;
using MovieApp.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DbOperations;
using Xunit;

namespace MovieApp.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory]
        [InlineData("   ")]
        [InlineData("      ")]
        [InlineData("Lor   ")]
        [InlineData("Lor")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            CreateGenreCommand command = new(null, null);
            command.Model = new CreateGenreModel()
            {
                Name = name
            };

            CreateGenreCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateGenreCommand command = new(null, null);
            command.Model = new CreateGenreModel()
            {
                Name = "Valid Name Sample"
            };

            CreateGenreCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}
