using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int MovieId { get; set; }

        public DeleteMovieCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }
        
        public void Handle()
        {
            var movie = _context.Movies.SingleOrDefault(x => x.Id == MovieId);

            if (movie is null)
                throw new InvalidOperationException("Film bulunamadı!");

            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }
    }
}