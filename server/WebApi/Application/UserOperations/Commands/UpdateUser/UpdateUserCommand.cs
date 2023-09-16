using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int UserId { get; set; }
        public UpdateUserModel Model { get; set; }

        public UpdateUserCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == UserId);

            if (user is null)
                throw new InvalidOperationException("Kullanıcı bulunamadı!");

            user.Name = Model.Name.Trim() != string.Empty ? Model.Name : user.Name;
            user.Surname = Model.Surname.Trim() != string.Empty ? Model.Surname : user.Surname;
            user.Password = Model.Password.Trim() != string.Empty ? Model.Password : user.Password;

            _context.SaveChanges();
        }
    }

    public class UpdateUserModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
    }
}