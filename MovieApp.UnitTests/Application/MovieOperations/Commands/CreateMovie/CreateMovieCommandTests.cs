using AutoMapper;
using FluentAssertions;
using MovieApp.UnitTests.TestSetup;
using System;
using System.Linq;
using WebApi.Application.MovieOperations.Commands.CreateMovie;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace MovieApp.UnitTests.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistMovieTitleIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            var movie = new Movie()
            {
                Title = "Test_WhenAlreadyExistMovieTitleIsGiven_InvalidOperationException_ShouldBeReturnError",
                Price = 100,
                GenreId = 1,
                DirectorId = 1,
                Year = DateTime.Now.Date.AddYears(-2)
            };
            _context.Movies.Add(movie);
            _context.SaveChanges();

            CreateMovieCommand command = new(_context, _mapper);
            command.Model = new CreateMovieModel() { Title = movie.Title };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film zaten mevcut");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Movie_ShouldBeCreated()
        {
            CreateMovieCommand command = new(_context, _mapper);
            CreateMovieModel model = new()
            {
                Title = "Test_WhenValidInputsAreGiven_Movie_ShouldBeCreated",
                Price = 30,
                GenreId = 1,
                DirectorId = 1,
                Year = DateTime.Now.Date.AddYears(-10)
            };
            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var movie = _context.Movies.SingleOrDefault(x => x.Title == model.Title);
            movie.Should().NotBeNull();
            movie.Title.Should().Be(model.Title);
            movie.Price.Should().Be(model.Price);
            movie.GenreId.Should().Be(model.GenreId);
            movie.DirectorId.Should().Be(model.DirectorId);
            movie.Year.Should().Be(model.Year);
        }
    }
}
