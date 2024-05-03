using Ecommerce_Application.Models;
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

        [Route("/Category/{categoryName}/{CategoryId:int}")]
        public async Task<IActionResult> ProductsByCategory(int CategoryId, string categoryName)
        {
            ViewBag.CategoryId = CategoryId;
            ViewBag.CategoryName = categoryName; 
           
            return View();
        }   

    }
}
