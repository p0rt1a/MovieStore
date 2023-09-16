using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Director
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string FullName { get; set; }
        public List<Movie> Movies { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}