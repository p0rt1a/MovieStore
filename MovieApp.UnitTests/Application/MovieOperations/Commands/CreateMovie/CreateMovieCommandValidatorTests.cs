using AutoMapper;
using FluentAssertions;
using MovieApp.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.MovieOperations.Commands.CreateMovie;
using WebApi.DbOperations;
using Xunit;

namespace MovieApp.UnitTests.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateMovieCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory]
        [InlineData("    ", 0, 0, 0)]
        [InlineData("XXX", 0, 0, 0)]
        [InlineData("ValidSample", 1, 0, 1)]
        [InlineData("ValidSample", 0, 1, 0)]
        [InlineData("ValidSample", 1, 0, 0)]
        [InlineData("ValidSample", 0, 0, 1)]
        [InlineData("", 1, 1, 1)]
        [InlineData("ValidSample", 0, 1, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, decimal price, int genreId, int directorId)
        {
            CreateMovieCommand command = new(null, null);
            command.Model = new CreateMovieModel()
            {
                Title = title,
                Price = price,
                GenreId = genreId,
                DirectorId = directorId,
                Year = DateTime.Now.Date.AddYears(-2)
            };

            CreateMovieCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenInvalidYearInputIsGiven_Validator_ShouldBeReturnError()
        {
            CreateMovieCommand command = new(null, null);
            command.Model = new CreateMovieModel()
            {
                Title = "Valid Title",
                GenreId = 1,
                DirectorId = 1,
                Price = 55,
                Year = DateTime.Now.Date
            };

            CreateMovieCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        //[Theory]
        //[InlineData("ValidTitleSample", 75.0, 1, 1)]
        //[InlineData("ValidTitleSample", 70, 1, 1)]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors(string title, decimal price, int genreId, int directorId)
        {
            CreateMovieCommand command = new(null, null);
            command.Model = new CreateMovieModel()
            {
                Title = title,
                Price = price,
                GenreId = genreId,
                DirectorId = directorId,
                Year = DateTime.Now.Date.AddYears(-2)
            };

            CreateMovieCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }

        public void WhenValidYearIsGiven_Validator_ShouldNotBeReturnError()
        {
            CreateMovieCommand command = new(null, null);
            command.Model = new CreateMovieModel()
            {
                Title = "Valid Title",
                GenreId = 1,
                DirectorId = 1,
                Year = DateTime.Now.Date.AddYears(-2),
                Price = 125
            };

            CreateMovieCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }
}
