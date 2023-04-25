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
        public DateTime Year { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int DirectorId { get; set; }
        public Director Director { get; set; }
        public decimal Price { get; set; }
        public ICollection<MovieActors> MovieActors { get; set; }
    }
}
