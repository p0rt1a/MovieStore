using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.Application.CustomerOperations.Commands.CreateToken;
using WebApi.Application.CustomerOperations.Commands.RefreshToken;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class CustomerController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CustomerController(IMovieStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody]CreateCustomerModel model)
        {
            CreateCustomerCommand command = new(_context, _mapper);
            command.Model = model;

            command.Handle();

            return Ok();
        }

        [HttpPost("connect/token")]
        public IActionResult CreateToken([FromBody]CreateTokenModel model)
        {
            CreateTokenCommand command = new(_context, _mapper, _configuration);
            command.Model = model;

            var token = command.Handle();

            return Ok(token);
        }

        [HttpGet("refreshToken")]
        public IActionResult RefreshToken([FromQuery]string token)
        {
            RefreshTokenCommand command = new(_context, _configuration);
            command.RefreshToken = token;

            var result = command.Handle();

            return Ok(result);
        }
    }
}
