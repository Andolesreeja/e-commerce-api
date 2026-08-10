using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_Api.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [Precision(18, 2)]
        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}