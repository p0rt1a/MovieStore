using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.CategoryOperations.Command.CreateCategory
{
    public class CreateCategoryCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateCategoryModel Model { get; set; }

        public CreateCategoryCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var category = _context.Categories.SingleOrDefault(x => x.Name == Model.Name);

            if (category is not null)
                throw new InvalidOperationException("Kategori zaten mevcut!");

            category = _mapper.Map<Category>(Model);
            
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
    }

    public class CreateCategoryModel
    {
        public string Name { get; set; }
    }
}