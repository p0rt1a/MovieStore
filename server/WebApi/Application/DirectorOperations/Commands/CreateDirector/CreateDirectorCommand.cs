using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateDirectorModel Model { get; set; }

        public CreateDirectorCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var director = _context.Directors.SingleOrDefault(x => x.FullName == Model.FullName);

            if (director is not null)
                throw new InvalidOperationException("Yönetmen zaten mevcut!");

            director = _mapper.Map<Director>(Model);

            _context.Directors.Add(director);
            _context.SaveChanges();
        }
    }

    public class CreateDirectorModel
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImageUrl { get; set; }
    }
}