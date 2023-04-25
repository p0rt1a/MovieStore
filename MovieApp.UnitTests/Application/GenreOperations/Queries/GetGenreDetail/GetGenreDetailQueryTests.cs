using AutoMapper;
using FluentAssertions;
using MovieApp.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DbOperations;
using Xunit;

namespace MovieApp.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenDoesntExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            GetGenreDetailQuery query = new(_context, _mapper);
            query.GenreId = 0;

            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Tür bulunamadı");
        }

        [Fact]
        public void WhenValidGenreIdIsGiven_Genre_ShouldBeReturn()
        {
            GetGenreDetailQuery query = new(_context, _mapper);
            query.GenreId = 1;

            var genre = FluentActions.Invoking(() => query.Handle()).Invoke();

            genre.Should().NotBeNull();
        }
    }
}
