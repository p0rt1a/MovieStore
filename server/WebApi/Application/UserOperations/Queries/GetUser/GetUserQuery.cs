using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.UserOperations.Queries.GetUser
{
    public class GetUserQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int UserId { get; set; }

        public GetUserQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public UserViewModel Handle()
        {
            var user = _context.Users
                .Include(x => x.Orders)
                .SingleOrDefault(x => x.Id == UserId);

            if (user is null)
                throw new InvalidOperationException("Kullanıcı bulunamadı!");

            var vm = _mapper.Map<UserViewModel>(user);

            var orders = _context.Orders
                .Where(x => x.UserId == UserId)
                .Include(x => x.Movie)
                .ToList<Order>();

            var orderVm = _mapper.Map<List<UserOrderViewModel>>(orders);

            vm.Orders = orderVm;

            return vm;
        }
    }

    public class UserViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<UserOrderViewModel> Orders { get; set; }
    }

    public class UserOrderViewModel
    {
        public int OrderId { get; set; }
        public string MovieImageUrl { get; set; }
        public string MovieTitle { get; set; }
        public string PurchaseDate { get; set; }
        public decimal Price { get; set; }
        public bool IsCancel { get; set; }
    }
}