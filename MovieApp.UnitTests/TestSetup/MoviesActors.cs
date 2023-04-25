using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DbOperations;
using WebApi.Entities;

namespace MovieApp.UnitTests.TestSetup
{
    public static class MoviesActors
    {
        public static void AddMovieActors(this MovieStoreDbContext context)
        {
            context.MovieActors.AddRange(
                new MovieActors()
                {
                    ActorId = 1,
                    MovieId = 1
                },
                new MovieActors()
                {
                    ActorId = 2,
                    MovieId = 1
                },
                new MovieActors()
                {
                    ActorId = 2,
                    MovieId = 3
                },
                new MovieActors()
                {
                    ActorId = 2,
                    MovieId = 2
                },
                new MovieActors()
                {
                    ActorId = 4,
                    MovieId = 2
                },
                new MovieActors()
                {
                    ActorId = 5,
                    MovieId = 2
                },
                new MovieActors()
                {
                    ActorId = 3,
                    MovieId = 3
                },
                new MovieActors()
                {
                    ActorId = 3,
                    MovieId = 1
                }
            );
        }
    }
}
