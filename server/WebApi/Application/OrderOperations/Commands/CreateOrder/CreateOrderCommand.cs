using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.Commands.CreateOrder
{
    public class CreateOrderCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateOrderModel Model { get; set; }

        public CreateOrderCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var order = _context.Orders.SingleOrDefault(x => x.UserId == Model.UserId && x.MovieId == Model.MovieId && !x.IsCancel);

            if (order is not null)
                throw new InvalidOperationException("Sipariş zaten kayıtlı!");

            order = _mapper.Map<Order>(Model);

            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }

    public class CreateOrderModel
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public decimal Price { get; set; }
    }
}