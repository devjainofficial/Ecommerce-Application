﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_Application.Models
{
    public class CartDetail
    {
        [Key]
        public int CartId { get; set; }
        public string Id { get; set; }
        [ForeignKey("Id")]
        public User User { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public ProductDetails ProductDetails { get; set; }
        public int Quantity { get; set; }
    }
}
