using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Application.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int ParentId {  get; set; }
        public List<ProductDetails> ProductDetails { get; set; }
    }
}
