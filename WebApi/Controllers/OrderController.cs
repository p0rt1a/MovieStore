using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.OrderOperations.Commands.CreateOrder;
using WebApi.Application.OrderOperations.Queries.GetOrders;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public OrderController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            GetOrdersQuery query = new(_context, _mapper);
            var result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody]CreateOrderModel model)
        {
            CreateOrderCommand command = new(_context, _mapper);
            command.Model = model;

            command.Handle();
            
            return Ok();
        }
    }
}
