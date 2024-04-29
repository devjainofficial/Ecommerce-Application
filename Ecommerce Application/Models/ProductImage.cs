using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Application.Models
{
    public class ProductImage
    {
        [Key]
        public int ImageId { get; set; }
        public int ProductId { get; set; }
        public string ImagePath { get; set; }

        public ProductDetails ProductDetails { get; set; }
    }
}
