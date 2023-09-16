using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Queries.GetMovies
{
    public class GetMoviesQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetMoviesQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<MoviesViewModel> Handle()
        {
            var movies = _context.Movies.Include(x => x.Category).ToList<Movie>();

            var vm = _mapper.Map<List<MoviesViewModel>>(movies);

            return vm;
        }
    }

    public class MoviesViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string PublishDate { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}