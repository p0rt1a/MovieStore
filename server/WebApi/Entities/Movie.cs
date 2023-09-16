using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Rerum temporibus nobis enim, magnam exercitationem totam a cum. Esse quis rerum iste veniam tempore iure quo error cum voluptatibus nihil. Debitis repellat officiis at minima inventore?";
        public string ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int DirectorId { get; set; }
        public Director Director { get; set; }
        public List<Order> Orders { get; set; }
        public decimal Price { get; set; }
    }
}