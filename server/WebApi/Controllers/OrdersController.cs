using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.OrderOperations.Commands.CreateOrder;
using WebApi.Application.OrderOperations.Commands.DeleteOrder;
using WebApi.Application.OrderOperations.Queries.GetOrders;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public OrdersController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            GetOrdersQuery query = new(_context, _mapper);
            
            var result = query.Handle();
        
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateOrderModel model)
        {
            CreateOrderCommand command = new(_context, _mapper);
            command.Model = model;

            CreateOrderCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Cancel(int id)
        {
            DeleteOrderCommand command = new(_context, _mapper);
            command.OrderId = id;

            DeleteOrderCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }
    }
}