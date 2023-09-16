using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int UserId { get; set; }

        public DeleteUserCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == UserId);

            if (user is null)
                throw new InvalidOperationException("Kullanıcı bulunamadı!");

            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}