using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

                context.Directors.AddRange(
                    new Director() {
                        FullName = "Frank Darabont",
                        DateOfBirth = new DateTime(1967, 06, 21),
                        ImageUrl = "https://moviestore534329.blob.core.windows.net/director-images/frankdarabont.jpg"
                    },
                    new Director() {
                        FullName = "Lana Wachowski",
                        DateOfBirth = new DateTime(1977, 10, 10),
                        ImageUrl = "https://moviestore534329.blob.core.windows.net/director-images/lanawachowski.jpg"
                    },
                    new Director() {
                        FullName = "James McTeigue",
                        DateOfBirth = new DateTime(1960, 05, 20),
                        ImageUrl = "https://moviestore534329.blob.core.windows.net/director-images/jamesmcteigue.jpg"
                    }
                );

                context.Categories.AddRange(
                    new Category() {
                        Name = "Drama"
                    },
                    new Category() {
                        Name = "Sci-Fi"
                    },
                    new Category() {
                        Name = "Action"
                    }
                );

                context.Movies.AddRange(
                    new Movie() {
                        Title = "Shawshank Redeption",
                        ImageUrl = "https://moviestore534329.blob.core.windows.net/movie-images/shawshank_redemption.jpg",
                        PublishDate = new DateTime(1995, 03, 10),
                        CategoryId = 1,
                        DirectorId = 1,
                        Price = 49
                    },
                    new Movie() {
                        Title = "Matrix",
                        ImageUrl = "https://moviestore534329.blob.core.windows.net/movie-images/matrix.jpg",
                        PublishDate = new DateTime(1999, 09, 03),
                        CategoryId = 2,
                        DirectorId = 2,
                        Price = 49
                    },
                    new Movie() {
                        Title = "V For Vendetta",
                        ImageUrl = "https://moviestore534329.blob.core.windows.net/movie-images/v.jpg",
                        PublishDate = new DateTime(1995, 03, 10),
                        CategoryId = 2,
                        DirectorId = 3,
                        Price = 39
                    },
                    new Movie() {
                        Title = "The Green Mile",
                        ImageUrl = "https://moviestore534329.blob.core.windows.net/movie-images/greenmile.jpg",
                        PublishDate = new DateTime(2000, 03, 17),
                        CategoryId = 1,
                        DirectorId = 1,
                        Price = 29
                    }
                );

                context.Users.AddRange(
                    new User() {
                        Name = "User",
                        Surname = "Demo",
                        Email = "demo@demo.com",
                        Password = "demo"
                    },
                    new User() {
                        Name = "Admin",
                        Surname = "Demo",
                        Email = "admin@demo.com",
                        Password = "demo"
                    }
                );

                context.Orders.AddRange(
                    new Order() {
                        UserId = 1,
                        MovieId = 1,
                        PurchaseDate = DateTime.Now.Date.AddDays(-2),
                        Price = 29
                    },
                    new Order() {
                        UserId = 1,
                        MovieId = 2,
                        PurchaseDate = DateTime.Now.Date.AddDays(-3),
                        Price = 49
                    },
                    new Order() {
                        UserId = 1,
                        MovieId = 3,
                        PurchaseDate = DateTime.Now.Date.AddDays(-10),
                        Price = 39,
                        IsCancel = true
                    }
                );

                context.SaveChanges();
            }
        }
    }
}