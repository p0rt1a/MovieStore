using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.Queries.GetDirector
{
    public class GetDirectorQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int DirectorId { get; set; }

        public GetDirectorQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public DirectorViewModel Handle()
        {
            var director = _context.Directors.Include(x => x.Movies).SingleOrDefault(x => x.Id == DirectorId);

            if (director is null)
                throw new InvalidOperationException("Yönetmen bulunamadı!");

            var movies = _context.Movies
                .Where(x => x.DirectorId == DirectorId)
                .Include(x => x.Category)
                .ToList<Movie>();

            var vm = _mapper.Map<DirectorViewModel>(director);

            var moviesVm = _mapper.Map<List<DirectorMovieViewModel>>(movies);

            vm.Movies = moviesVm;

            return vm;
        }
    }

    public class DirectorViewModel
    {
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public string DateOfBirth { get; set; }
        public List<DirectorMovieViewModel> Movies { get; set; }
    }

    public class DirectorMovieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }
    }
}