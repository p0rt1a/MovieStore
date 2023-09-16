using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now.Date;
        public decimal Price { get; set; }
        public bool IsCancel { get; set; } = false;
        //TODO: Cancel date will be fine!
    }
}