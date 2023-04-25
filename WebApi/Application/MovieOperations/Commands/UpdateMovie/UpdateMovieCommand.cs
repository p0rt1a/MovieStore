using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DbOperations;

namespace WebApi.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommand
    {
        private readonly IMovieStoreDbContext _dbContext;

        public int MovieId { get; set; }
        public UpdateMovieModel Model { get; set; }

        public UpdateMovieCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var movie = _dbContext.Movies.SingleOrDefault(x => x.Id == MovieId);

            if (movie is null)
                throw new InvalidOperationException("Film bulunamadı");

            movie.Title = string.IsNullOrEmpty(Model.Title.Trim()) ? movie.Title : Model.Title;
            movie.Price = Model.Price > 0 ? Model.Price : movie.Price;

            _dbContext.SaveChanges();
        }
    }

    public class UpdateMovieModel
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}
