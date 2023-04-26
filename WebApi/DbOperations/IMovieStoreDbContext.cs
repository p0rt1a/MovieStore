using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public interface IMovieStoreDbContext
    {
        DbSet<Movie> Movies { get; set; }
        DbSet<Director> Directors { get; set; }
        DbSet<Actor> Actors { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<MovieActors> MovieActors { get; set; }
        DbSet<Customer> Customers { get; set; }

        int SaveChanges();
    }
}
