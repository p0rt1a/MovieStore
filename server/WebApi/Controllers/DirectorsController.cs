using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DirectorOperations.Commands.CreateDirector;
using WebApi.Application.DirectorOperations.Commands.DeleteDirector;
using WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using WebApi.Application.DirectorOperations.Queries.GetDirector;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DirectorsController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public DirectorsController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            GetDirectorQuery query = new(_context, _mapper);
            query.DirectorId = id;

            var result = query.Handle();

            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody]CreateDirectorModel model)
        {
            CreateDirectorCommand command = new(_context, _mapper);
            command.Model = model;

            CreateDirectorCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update([FromBody]UpdateDirectorModel model, int id)
        {
            UpdateDirectorCommand command = new(_context);
            command.DirectorId = id;
            command.Model = model;

            UpdateDirectorCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            
            command.Handle();

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DeleteDirectorCommand command = new(_context);
            command.DirectorId = id;

            DeleteDirectorCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }
    }
}