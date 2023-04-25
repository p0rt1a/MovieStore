using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Common;
using WebApi.DbOperations;

namespace MovieApp.UnitTests.TestSetup
{
    public class CommonTestFixture
    {
        public MovieStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }

        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<MovieStoreDbContext>().UseInMemoryDatabase(databaseName: "MovieStoreTestDb").Options;
            Context = new MovieStoreDbContext(options);

            Context.Database.EnsureCreated();

            Context.AddActors();
            Context.AddGenres();
            Context.AddDirectors();
            Context.AddMovies();
            Context.AddMovieActors();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(config => { config.AddProfile<MappingProfile>(); }).CreateMapper();
        }
    }
}
