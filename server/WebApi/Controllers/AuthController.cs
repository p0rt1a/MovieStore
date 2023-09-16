using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Application.AuthOperations.Commands.CreateRefreshToken;
using WebApi.Application.AuthOperations.Commands.CreateToken;
using WebApi.DbOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(IMovieStoreDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody]LoginModel model)
        {
            CreateTokenCommand command = new(_context, _configuration);
            command.Model = model;

            var token = command.Handle();

            return Ok(token);
        }

        [HttpPost("refreshToken")]
        public ActionResult<Token> RefreshToken([FromQuery]string token)
        {
            CreateRefreshTokenCommand command = new(_context, _configuration);
            command.RefreshToken = token;

            var newToken = command.Handle();

            return Ok(newToken);
        }
    }
}