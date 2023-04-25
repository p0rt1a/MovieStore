using AutoMapper;
using System;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateGenreModel Model { get; set; }

        public CreateGenreCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            if (_dbContext.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower()))
                throw new InvalidOperationException("Tür zaten mevcut");

            var createdGenre = _mapper.Map<Genre>(Model);

            _dbContext.Genres.Add(createdGenre);
            _dbContext.SaveChanges();
        }
    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}
