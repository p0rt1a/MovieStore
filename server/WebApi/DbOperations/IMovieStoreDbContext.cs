using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public interface IMovieStoreDbContext
    {
        DbSet<Movie> Movies { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Director> Directors { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Order> Orders { get; set; }

        int SaveChanges();
    }
}