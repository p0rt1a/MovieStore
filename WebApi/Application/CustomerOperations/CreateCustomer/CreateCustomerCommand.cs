using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.CustomerOperations.CreateCustomer
{
    public class CreateCustomerCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateCustomerModel Model { get; set; }

        public CreateCustomerCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var customer = _dbContext.Customers.SingleOrDefault(x => x.Name.ToLower() == Model.Name.ToLower());

            if (customer is not null)
                throw new InvalidOperationException("Kullanıcı zaten mevcut");

            var createdCustomer = _mapper.Map<Customer>(Model);
            _dbContext.Customers.Add(createdCustomer);
            _dbContext.SaveChanges();
        }
    }

    public class CreateCustomerModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
