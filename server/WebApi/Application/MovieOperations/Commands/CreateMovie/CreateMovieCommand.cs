using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateMovieModel Model { get; set; }

        public CreateMovieCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var movie = _context.Movies.SingleOrDefault(x => x.Title == Model.Title);

            if (movie is not null)
                throw new InvalidOperationException("Film zaten mevcut!");

            movie = _mapper.Map<Movie>(Model);

            _context.Movies.Add(movie);
            _context.SaveChanges();
        }
    }

    public class CreateMovieModel
    {
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public int DirectorId { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}