using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Order> Orders { get; set; }
        //TODO: Add a role for admin panel

        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireDate { get; set; }
    }
}