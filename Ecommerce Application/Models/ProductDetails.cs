using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string ThumbImage {  get; set; }
        [ValidateNever]
        public List<ProductImage> ProductImages { get; set; }
        [ValidateNever]
        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }
    }
}
