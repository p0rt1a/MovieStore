using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.CategoryOperations.Command.DeleteCategory
{
    public class DeleteCategoryCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int CategoryId { get; set; }

        public DeleteCategoryCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var category = _context.Categories.SingleOrDefault(x => x.Id == CategoryId);

            if (category is null)
                throw new InvalidOperationException("Kategori bulunamadı!");

            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}