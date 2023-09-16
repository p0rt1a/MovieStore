using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.Application.UserOperations.Commands.DeleteUser;
using WebApi.Application.UserOperations.Commands.UpdateUser;
using WebApi.Application.UserOperations.Queries.GetUser;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public UsersController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            GetUserQuery query = new(_context, _mapper);
            query.UserId = id;

            GetUserQueryValidator validator = new();
            validator.ValidateAndThrow(query);

            var result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateUserModel model)
        {
            CreateUserCommand command = new(_context, _mapper);
            command.Model = model;

            CreateUserCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update([FromBody]UpdateUserModel model, int id)
        {
            UpdateUserCommand command = new(_context);
            command.UserId = id;
            command.Model = model;

            UpdateUserCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DeleteUserCommand command = new(_context);
            command.UserId = id;

            DeleteUserCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }
    }
}