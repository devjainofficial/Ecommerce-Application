using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Application.Models
{
    public class ProductDetails
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int SellingPrice { get; set; }
        public string Description { get; set; }
        public string BrandName { get; set; }
        public int CategoryId { get; set; }
        public int SKU { get; set; }
        public List<ProductImage> ProductImage { get; set; }
        public Category? Category { get; set; }
    }
}
