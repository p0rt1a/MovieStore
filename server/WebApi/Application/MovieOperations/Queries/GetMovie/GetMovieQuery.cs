using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.Application.MovieOperations.Queries.GetMovie
{
    public class GetMovieQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int MovieId { get; set; }

        public GetMovieQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public MovieViewModel Handle()
        {
            var movie = _context.Movies
                .Include(x => x.Category)
                .Include(x => x.Director)
                .SingleOrDefault(x => x.Id == MovieId);

            if (movie is null)
                throw new InvalidOperationException("Film mevcut değil!");

            var vm = _mapper.Map<MovieViewModel>(movie);

            return vm;
        }
    }

    public class MovieViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string PublishDate { get; set; }
        public decimal Price { get; set; }
        public string Director { get; set; }
        public string Category { get; set; }
    }
}