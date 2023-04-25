using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DbOperations;
using WebApi.Entities;

namespace MovieApp.UnitTests.TestSetup
{
    public static class Movies
    {
        public static void AddMovies(this MovieStoreDbContext context)
        {
            context.Movies.AddRange(
                new Movie()
                {
                    Title = "The Shawshank Redemption",
                    Year = new DateTime(1994, 02, 15),
                    Price = 100,
                    GenreId = 1,
                    DirectorId = 1
                },
                new Movie()
                {
                    Title = "Casablanca",
                    Year = new DateTime(2007, 07, 21),
                    Price = 220,
                    GenreId = 2,
                    DirectorId = 3
                },
                new Movie()
                {
                    Title = "Notorious",
                    Year = new DateTime(1992, 01, 01),
                    Price = 80,
                    GenreId = 2,
                    DirectorId = 2
                }
            );
        }
    }
}
