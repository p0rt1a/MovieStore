using AutoMapper;
using FluentAssertions;
using MovieApp.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.MovieOperations.Commands.CreateMovie;
using WebApi.Application.MovieOperations.Commands.DeleteMovie;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace MovieApp.UnitTests.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenDoesntExistMovieIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            DeleteMovieCommand command = new(_context);
            command.MovieId = 0;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film bulunamadı.");
        }

        [Fact]
        public void WhenValidMovieIdIsGiven_Movie_ShouldBeDeleted()
        {
            var movie = new Movie()
            {
                Title = "Test_WhenAlreadyExistMovieIdIsGiven_Movie_ShouldBeDeleted",
                Price = 17,
                GenreId = 1,
                DirectorId = 1,
                Year = DateTime.Now.Date.AddYears(-10)
            };
            _context.Movies.Add(movie);
            _context.SaveChanges();

            DeleteMovieCommand command = new(_context);
            command.MovieId = 4;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var deletedMovie = _context.Movies.SingleOrDefault(x => x.Id == command.MovieId);
            deletedMovie.Should().BeNull();
        }
    }
}
