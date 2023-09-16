using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using WebApi.DbOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.AuthOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IConfiguration _configuration;
        public LoginModel Model { get; set; }

        public CreateTokenCommand(IMovieStoreDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);

            if (user is not null)
            {
                TokenHandler tokenHandler = new TokenHandler(_configuration);
                Token token = tokenHandler.CreateAccessToken(user);
                
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.ExpirationDate.AddHours(10);

                _context.SaveChanges();

                return token;
            }

            else
                throw new InvalidOperationException("Kullanıcı bulunamadı!");
        }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}