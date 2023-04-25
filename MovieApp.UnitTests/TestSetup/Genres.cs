using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DbOperations;
using WebApi.Entities;

namespace MovieApp.UnitTests.TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this MovieStoreDbContext context)
        {
            context.Genres.AddRange(
                new Genre()
                {
                    Name = "Science Ficiton"
                },
                new Genre()
                {
                    Name = "Thriller"
                }
            );
        }
    }
}
