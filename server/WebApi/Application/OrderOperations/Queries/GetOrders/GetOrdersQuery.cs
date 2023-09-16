using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.Queries.GetOrders
{
    public class GetOrdersQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetOrdersQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<OrdersViewModel> Handle()
        {
            var orders = _context.Orders
                .Include(x => x.Movie)
                .Include(x => x.User)
                .ToList<Order>();

            var vm = _mapper.Map<List<OrdersViewModel>>(orders);

            return vm;
        }
    }

    public class OrdersViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string User { get; set; }
        public int MovieId { get; set; }
        public string Movie { get; set; }
        public string PurchaseDate { get; set; }
        public decimal Price { get; set; }
        public bool IsCancel { get; set; }
    }
}