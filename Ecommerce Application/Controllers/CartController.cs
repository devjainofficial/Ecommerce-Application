using Ecommerce_Application.Migrations;
using Ecommerce_Application.Models;
using Ecommerce_Application.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Ecommerce_Application.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartController(ICartRepository cartRepository, IProductRepository productRepository, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult CartIndex()
        {

            return View();
        }

        public async Task<IActionResult> AddCart(int id)
        {
            var principal = _httpContextAccessor.HttpContext.User;
            var product = await _productRepository.GetById(id);
            var UserId = _userManager.GetUserId(principal);
            var User = await _userManager.GetUserAsync(principal);
            var cart = await _cartRepository.GetAllDetails(UserId);
            var cartCount = cart.Count();
            ViewBag.UserId = UserId;
            if (cartCount == 0)
            {

                List<CartDetail> cartList = new List<CartDetail>()
                {
                    new CartDetail
                    {
                        Id = UserId,
                        ProductId = id,
                        ProductDetails = product,
                        User = User,
                        Quantity = 1
                    }
                };

                if (User != null)
                {
                    foreach (var cartDetail in cartList)
                    {
                        _cartRepository.InsertCart(cartDetail);
                    }
                    _cartRepository.SaveChangesAsync();

                }

            }
            else
            {
                List<CartDetail> dataCart = cart;
                bool check = true;
                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].ProductId == id)
                    {
                        dataCart[i].Quantity++;
                        check = false;
                    }
                }
                if (check)
                {
                    dataCart.Add(new CartDetail
                    {
                        Id = UserId,
                        ProductId = id,
                        ProductDetails = product,
                        User = User,
                        Quantity = 1
                    });
                }
                if (User != null)
                {
                    foreach (var cartDetail in dataCart)
                    {
                        _cartRepository.UpdateCart(cartDetail);
                    }
                    _cartRepository.SaveChangesAsync();

                }
               
            }
            var status = new Status();
            status.StatusCode = 1;
            status.Message = "Your Product Added Successfuly";
            return Ok(status);
           
        }
       
        //[HttpPost]
        //public IActionResult updateCart(int id, int quantity)
        //{
        //    var cart = HttpContext.Session.GetString("cart");
        //    if (cart != null)
        //    {
        //        List<CartDetail> dataCart = JsonConvert.DeserializeObject<List<CartDetail>>(cart);
        //        if (quantity > 0)
        //        {
        //            for (int i = 0; i < dataCart.Count; i++)
        //            {
        //                if (dataCart[i].ProductId == id)
        //                {
        //                    dataCart[i].Quantity = quantity;
        //                }
        //            }


        //            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
        //        }
        //        var cart2 = HttpContext.Session.GetString("cart");
        //        return Ok(quantity);
        //    }
        //    return BadRequest();

        //}

        //public IActionResult deleteCart(int id)
        //{
        //    var cart = HttpContext.Session.GetString("cart");
        //    if (cart != null)
        //    {
        //        List<CartDetail> dataCart = JsonConvert.DeserializeObject<List<CartDetail>>(cart);

        //        for (int i = 0; i < dataCart.Count; i++)
        //        {
        //            if (dataCart[i].ProductId == id)
        //            {
        //                dataCart.RemoveAt(i);
        //            }
        //        }
        //        HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
        //        return RedirectToAction(nameof(ListCart));
        //    }
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
