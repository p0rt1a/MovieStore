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
    public class UpdateMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenDoesntExistMovieIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            UpdateMovieCommand command = new(_context);
            command.MovieId = 0;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film bulunamadı");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Movie_ShouldBeUpdated()
        {
            UpdateMovieCommand command = new(_context);
            UpdateMovieModel model = new UpdateMovieModel()
            {
                Title = "Test_WhenValidInputsAreGiven_Movie_ShouldBeUpdated",
                Price = 27
            };
            command.Model = model;
            command.MovieId = 1;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var movie = _context.Movies.SingleOrDefault(x => x.Id == command.MovieId);
            movie.Title.Should().Be(model.Title);
            movie.Price.Should().Be(model.Price);
        }
    }
}
