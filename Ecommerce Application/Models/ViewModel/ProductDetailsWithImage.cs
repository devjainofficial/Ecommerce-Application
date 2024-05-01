namespace Ecommerce_Application.Models.ViewModel
{
    public class ProductDetailsWithImage
    {
        public List<ProductDetails> ProductDetails { get; set; } = new List<ProductDetails>();
        public List<ProductImage> ProductImage { get; set; } = new List<ProductImage>();

    }
}
