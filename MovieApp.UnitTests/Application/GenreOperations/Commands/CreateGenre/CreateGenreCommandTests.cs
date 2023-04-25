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
using WebApi.Entities;
using Xunit;

namespace MovieApp.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            var model = new Genre()
            {
                Name = "Test_WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturnError"
            };
            _context.Genres.Add(model);
            _context.SaveChanges();

            CreateGenreCommand command = new(_context, _mapper);
            command.Model = new CreateGenreModel()
            {
                Name = model.Name
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Tür zaten mevcut");
        }

        [Fact]
        public void WhenValidGenreNameIsGiven_Genre_ShouldBeCreated()
        {
            CreateGenreCommand command = new(_context, _mapper);
            command.Model = new CreateGenreModel()
            {
                Name = "Test_WhenValidGenreNameIsGiven_Genre_ShouldBeCreated"
            };

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var genre = _context.Genres.SingleOrDefault(x => x.Name == command.Model.Name);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(command.Model.Name);
        }
    }
}
