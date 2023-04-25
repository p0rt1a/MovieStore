using AutoMapper;
using FluentAssertions;
using MovieApp.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace MovieApp.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenDoesntExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            DeleteGenreCommand command = new(_context);
            command.GenreId = 0;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>("Tür bulunamadı");
        }

        [Fact]
        public void WhenValidGenreIdIsGiven_Genre_ShouldBeDeleted()
        {
            var genre = new Genre()
            {
                Name = "Test_WhenValidGenreIdIsGiven_Genre_ShouldBeDeleted"
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            DeleteGenreCommand command = new(_context);
            command.GenreId = 3;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var deletedGenre = _context.Genres.SingleOrDefault(x => x.Id == command.GenreId);
            deletedGenre.Should().BeNull();
        }
    }
}
