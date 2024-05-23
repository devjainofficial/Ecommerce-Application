using Ecommerce_Application.Models;
using Ecommerce_Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Ecommerce_Application.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        
        public ProductController( IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [Route("/Product/{ProductName}/{ProductId}")]
        public async Task<IActionResult> ContentDetails(int ProductId, string ProductName)
        {
            var content = await _productRepository.GetById(Convert.ToInt32(ProductId));
            return View(content);
        }
 
    }
}
