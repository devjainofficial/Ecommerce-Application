using Ecommerce_Application.Models;
using Ecommerce_Application.Repositories;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client.Extensions.Msal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stripe.Climate;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

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


        public async Task<IActionResult> AddCart(int id, string cartData)
        {
            var principal = _httpContextAccessor.HttpContext.User;
            var product = await _productRepository.GetById(id);
            var userId = _userManager.GetUserId(principal);
            var user = await _userManager.GetUserAsync(principal);
            var cart = await _cartRepository.GetAllDetails(userId);
            var cartCount = cart.Count();
            var localCart = new List<CartDetail>();
            if (userId != null && cartData != null)
            {

                localCart = JsonConvert.DeserializeObject<List<CartDetail>>(cartData);

                foreach (var cartItem in localCart)
                {
                    var existingCartItem = cart.FirstOrDefault(c => c.ProductId == cartItem.ProductId);
                    if (existingCartItem == null)
                    {
                        var newCartItem = new CartDetail
                        {
                            Id = userId,
                            ProductId = cartItem.ProductId,
                            ProductDetails = product,
                            Quantity = cartItem.Quantity
                        };
                        _cartRepository.InsertCart(newCartItem);
                    }
                    else
                    {
                        existingCartItem.Quantity += cartItem.Quantity;
                        _cartRepository.UpdateCart(existingCartItem);
                    }
                }
            }

            else
            {

                if (cartCount == 0)
                {
                    var newCartItem = new CartDetail
                    {
                        Id = userId,
                        ProductId = id,
                        ProductDetails = product,
                        User = user,
                        Quantity = 1
                    };
                    if (user != null)
                    {
                        _cartRepository.InsertCart(newCartItem);
                    }
                    else
                    {
                        localCart.Add(newCartItem);
                    }
                }
                else
                {
                    var existingCartItem = cart.FirstOrDefault(c => c.ProductId == id);
                    if (existingCartItem != null)
                    {
                        existingCartItem.Quantity++;
                        if (user != null)
                        {
                            _cartRepository.UpdateCart(existingCartItem);
                        }
                    }
                    else
                    {
                        var newCartItem = new CartDetail
                        {
                            Id = userId,
                            ProductId = id,
                            ProductDetails = product,
                            User = user,
                            Quantity = 1
                        };
                        if (user != null)
                        {
                            _cartRepository.InsertCart(newCartItem);
                        }
                        else
                        {
                            localCart.Add(newCartItem);
                        }
                    }
                }

            }
            var updatedCart = new List<CartDetail>();
            if (user != null)
            {
                _cartRepository.SaveChangesAsync();
                updatedCart = await _cartRepository.GetAllDetails(userId);
            }
            else
            {
                updatedCart = localCart;
            }
            var status = new Status
            {
                StatusCode = 1,
                Message = "Your product was added successfully"
            };
            var response = new
            {
                status = status,
                count = updatedCart.Count,
                cart = updatedCart,
                user = user
            };
            return Ok(response);
        }

        public async Task<IActionResult> unaryOperation(int id, string operation)
        {
            var principal = _httpContextAccessor.HttpContext.User;
            var product = await _productRepository.GetById(id);
            var UserId = _userManager.GetUserId(principal);
            var User = await _userManager.GetUserAsync(principal);
            var cart = await _cartRepository.GetAllDetails(UserId);
            var cartCount = cart.Count();
            ViewBag.UserId = UserId;
            List<CartDetail> dataCart = cart;
            if (cartCount != 0)
            {
                if (operation == "plus")
                {

                    for (int i = 0; i < dataCart.Count; i++)
                    {
                        if (dataCart[i].ProductId == id)
                        {
                            dataCart[i].Quantity++;
                        }
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
                if (operation == "minus")
                {
                    for (int i = 0; i < dataCart.Count; i++)
                    {
                        if (dataCart[i].ProductId == id)
                        {
                            dataCart[i].Quantity--;
                            if (dataCart[i].Quantity > 0)
                            {
                                _cartRepository.SaveChangesAsync();
                                return RedirectToAction("CartIndex");
                            }
                            else
                            {
                                _cartRepository.DeleteProductIdFromCart(id);
                                _cartRepository.SaveChangesAsync();
                                return RedirectToAction("CartIndex");
                            }
                        }
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

            }

            return RedirectToAction("CartIndex");

        }


        public async Task<IActionResult> OrderSuccess()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            var UserId = _userManager.GetUserId(principal);
            var User = await _userManager.GetUserAsync(principal);
            var cart = await _cartRepository.GetAllDetails(UserId);

            if (User != null)
            {
                foreach (var cartDetail in cart)
                {
                    _cartRepository.DeleteCart(cartDetail);
                    _cartRepository.SaveChangesAsync();
                }
            }
            return View();
        }


    }
}


