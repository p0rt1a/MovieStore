using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using WebApi.Application.AuthOperations.Commands.CreateToken;
using WebApi.DbOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.AuthOperations.Commands.CreateRefreshToken
{
    public class CreateRefreshTokenCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IConfiguration _configuration;
        public string RefreshToken { get; set; }

        public CreateRefreshTokenCommand(IMovieStoreDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _context.Users.SingleOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);

            if (user is null)
                throw new InvalidOperationException("Token süresi dolmuş!");

            TokenHandler tokenHandler = new TokenHandler(_configuration);
            Token token = tokenHandler.CreateAccessToken(user);

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.ExpirationDate.AddHours(10);

            _context.SaveChanges();

            return token;
        }
    }
}