using Ecommerce_Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Application.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> ProductsByCategory(int CategoryId)
        {
            ViewBag.CategoryId = CategoryId;
            ViewData["CategoryId"] = CategoryId;
            var products = await _categoryRepository.GetAllProductsByCategoriesAsync(CategoryId);
            return View(products);
        }



        /* [Route("Category/ProductsByCategory/{CategoryId}")]
         public async Task<IActionResult> ProductsByCategory( int[] CategoryId)
         {
             ViewBag.CategoryId = CategoryId;
             ViewData["CategoryId"] = CategoryId;
             var products = await _categoryRepository.GetAllProductsByCategoriesAsync(CategoryId);
             return View(products);
         }*/
    }
}
