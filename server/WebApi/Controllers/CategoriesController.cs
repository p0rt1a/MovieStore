using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.CategoryOperations.Command.CreateCategory;
using WebApi.Application.CategoryOperations.Command.DeleteCategory;
using WebApi.Application.CategoryOperations.Command.UpdateCategory;
using WebApi.Application.CategoryOperations.Queries.GetCategories;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            GetCategoriesQuery query = new(_context, _mapper);
            var result = query.Handle();
        
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody]CreateCategoryModel model)
        {
            CreateCategoryCommand command = new(_context, _mapper);
            command.Model = model;

            CreateCategoryCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update([FromBody]UpdateCategoryModel model, int id)
        {
            UpdateCategoryCommand command = new(_context);
            command.Model = model;
            command.CategoryId = id;

            UpdateCategoryCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DeleteCategoryCommand command = new(_context);
            command.CategoryId = id;

            DeleteCategoryCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }
    }
}