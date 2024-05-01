using Ecommerce_Application.Models;
using Ecommerce_Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Application.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCRUDRepository _productDetails;
        public ProductController(IProductRepository productRepository, IProductCRUDRepository productDetails)
        {
            _productRepository = productRepository;
            _productDetails = productDetails;
        }
        public async Task<IActionResult> ContentDetails(int ProductId)
        {
            var content = await _productDetails.GetById(Convert.ToInt32(ProductId));
            return View(content);
        }


    }
}
