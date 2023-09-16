using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int DirectorId { get; set; }
        public UpdateDirectorModel Model { get; set; }

        public UpdateDirectorCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var director = _context.Directors.SingleOrDefault(x => x.Id == DirectorId);

            if (director is null)
                throw new InvalidOperationException("Yönetmen bulunamadı!");

            director.FullName = Model.FullName.Trim() != string.Empty ? Model.FullName : director.FullName;
            director.ImageUrl = Model.ImageUrl.Trim() != string.Empty ? Model.ImageUrl : director.ImageUrl;

            _context.SaveChanges();
        }
    }

    public class UpdateDirectorModel
    {
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
    }
}