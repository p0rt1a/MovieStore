using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.CategoryOperations.Queries.GetCategories
{
    public class GetCategoriesQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoriesQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<CategoriesViewModel> Handle()
        {
            var categories = _context.Categories.ToList<Category>();

            var vm = _mapper.Map<List<CategoriesViewModel>>(categories);

            return vm;
        }
    }

    public class CategoriesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}