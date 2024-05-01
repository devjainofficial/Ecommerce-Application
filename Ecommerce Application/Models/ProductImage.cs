using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_Application.Models
{
    public class ProductImage
    {
        [Key]
        public int ImageId { get; set; }
        public int ProductId { get; set; }
        public string ImagePath { get; set; }
        [ForeignKey(nameof(ProductId))]
        [ValidateNever]
        public ProductDetails Product { get; set; }
    }
}
