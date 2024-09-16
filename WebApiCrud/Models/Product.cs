﻿using System.ComponentModel.DataAnnotations;

namespace WebApiCrud.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Qty { get; set; }

        [Required]
        public Unit Unit { get; set; }

        public string? Description { get; set; }
    }
}
