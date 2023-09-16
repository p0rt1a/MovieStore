using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommand
    {
        private readonly IMovieStoreDbContext _context;
        public UpdateMovieModel Model { get; set; }
        public int MovieId { get; set; }

        public UpdateMovieCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var movie = _context.Movies.SingleOrDefault(x => x.Id == MovieId);

            if (movie is null)
                throw new InvalidOperationException("Film bulunamadı!");

            movie.Title = Model.Title.Trim() != string.Empty ? Model.Title : movie.Title;
            movie.Description = Model.Description.Trim() != string.Empty ? Model.Description : movie.Description;
            movie.Price = Model.Price != default ? Model.Price : movie.Price;
            movie.CategoryId = Model.CategoryId;
            movie.PublishDate = Model.PublishDate != default ? Model.PublishDate : movie.PublishDate;

            _context.SaveChanges();
        }
    }

    public class UpdateMovieModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
    }
}