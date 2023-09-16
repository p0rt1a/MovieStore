using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.CategoryOperations.Command.UpdateCategory
{
    public class UpdateCategoryCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int CategoryId { get; set; }
        public UpdateCategoryModel Model { get; set; }

        public UpdateCategoryCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var category = _context.Categories.SingleOrDefault(x => x.Id == CategoryId);

            if (category is null)
                throw new InvalidOperationException("Kategori bulunamadı!");

            category.Name = Model.Name.Trim() != string.Empty ? Model.Name : category.Name;

            _context.SaveChanges();
        }
    }

    public class UpdateCategoryModel
    {
        public string Name { get; set; }
    }
}