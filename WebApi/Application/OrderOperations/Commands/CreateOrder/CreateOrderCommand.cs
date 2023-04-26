using AutoMapper;
using System;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.Commands.CreateOrder
{
    public class CreateOrderCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateOrderModel Model { get; set; }

        public CreateOrderCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var movie = _dbContext.Movies.SingleOrDefault(x => x.Id == Model.MovieId);

            if (movie is null)
                throw new InvalidOperationException("Film bulunamadı");

            var customer = _dbContext.Customers.SingleOrDefault(x => x.Id == Model.CustomerId);

            if (customer is null)
                throw new InvalidOperationException("Müşteri bulunamadı");

            var order = _mapper.Map<Order>(Model);
            order.Price = movie.Price;
            order.PurchaseDate = DateTime.Now;

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
        }
    }

    public class CreateOrderModel
    {
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
    }
}
