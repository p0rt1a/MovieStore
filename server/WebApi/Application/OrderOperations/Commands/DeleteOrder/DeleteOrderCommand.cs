using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.OrderOperations.Commands.DeleteOrder
{
    public class DeleteOrderCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int OrderId { get; set; }

        public DeleteOrderCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var order = _context.Orders.SingleOrDefault(x => x.Id == OrderId);

            if (order.IsCancel)
                throw new InvalidOperationException("Sipariş bulunamadı!");

            order.IsCancel = true;

            _context.SaveChanges();
        }
    }
}