using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
            {
                if (context.Movies.Any())
                    return;

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

                context.Directors.AddRange(
                        new Director()
                        {
                            Name = "Steven",
                            Surname = "Spielberg"
                        },
                        new Director()
                        {
                            Name = "Alfred",
                            Surname = "Hitchcock"
                        },
                        new Director()
                        {
                            Name = "Akira",
                            Surname = "Kurosawa"
                        }
                    );

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

                context.Actors.AddRange(
                        new Actor()
                        {
                            Name = "James",
                            Surname = "Cagney",
                        },
                        new Actor()
                        {
                            Name = "James",
                            Surname = "Stewart"
                        },
                        new Actor()
                        {
                            Name = "Cary",
                            Surname = "Grant"
                        },
                        new Actor()
                        {
                            Name = "Henry",
                            Surname = "Fonda"
                        },
                        new Actor()
                        {
                            Name = "Marlon",
                            Surname = "Brondo"
                        }
                    );

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

                context.SaveChanges();
            }
        }
    }
}
