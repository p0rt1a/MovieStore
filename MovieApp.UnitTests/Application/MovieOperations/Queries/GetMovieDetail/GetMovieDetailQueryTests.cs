using AutoMapper;
using FluentAssertions;
using MovieApp.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using WebApi.DbOperations;
using Xunit;

namespace MovieApp.UnitTests.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetMovieDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenDoesntExistMovieIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            GetMovieDetailQuery query = new(_context, _mapper);
            query.MovieId = 0;

            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film bulunamadı");
        }

        [Fact]
        public void WhenValidMovieIdIsGiven_Movie_ShouldBeReturn()
        {
            GetMovieDetailQuery query = new(_context, _mapper);
            query.MovieId = 1;

            var movie = FluentActions.Invoking(() => query.Handle()).Invoke();

            movie.Should().NotBeNull();
        }
    }
}
