using AutoMapper;
using FluentAssertions;
using MovieApp.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DbOperations;
using Xunit;

namespace MovieApp.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenDoesntExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            UpdateGenreCommand command = new(_context, _mapper);
            command.GenreId = 0;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Tür bulunamadı");
        }

        [Fact]
        public void WhenValidGenreIdIsGiven_Genre_ShouldBeUpdated()
        {
            UpdateGenreCommand command = new(_context, _mapper);
            command.GenreId = 1;
            command.Model = new UpdateGenreModel()
            {
                Name = "Test_WhenValidGenreIdIsGiven_Genre_ShouldBeUpdated"
            };

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var genre = _context.Genres.SingleOrDefault(x => x.Name == command.Model.Name);
            genre.Name.Should().Be(command.Model.Name);
        }
    }
}
