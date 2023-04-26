using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DbOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.CustomerOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public string RefreshToken { get; set; }

        public RefreshTokenCommand(IMovieStoreDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var customer = _dbContext.Customers.SingleOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);

            if (customer is not null)
            {
                TokenHandler handler = new(_configuration);
                Token token = handler.CreateAccessToken();

                customer.RefreshToken = token.RefreshToken;
                customer.RefreshTokenExpireDate = token.ExpirationDate.AddMinutes(5);

                _dbContext.SaveChanges();

                return token;
            }
            else
                throw new InvalidOperationException("Geçerli bir token bilgisi bulunamadı");
        }
    }

    
}
