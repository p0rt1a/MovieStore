using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.Queries.GetOrders
{
    public class GetOrdersQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetOrdersQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<OrderViewModel> Handle()
        {
            var orders = _dbContext.Orders.Include(x => x.Movie).Include(x => x.Customer).ToList<Order>();

            var vm = _mapper.Map<List<OrderViewModel>>(orders);

            return vm;
        }
    }

    public class OrderViewModel
    {
        public string MovieName { get; set; }
        public string CustomerName { get; set; }
        public decimal Price { get; set; }
        public string PurchaseDate { get; set; }
    }
}
