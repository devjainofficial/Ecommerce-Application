using Ecommerce_Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Application.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository cartRepository;

        public CartController(ICartRepository _cartRepository)
        {
            cartRepository = _cartRepository;
        }
        public IActionResult CartIndex()
        {
            return View();  
        }
    }
}
