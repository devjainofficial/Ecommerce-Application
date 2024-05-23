using Ecommerce_Application.Models;
using Ecommerce_Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.IdentityModel.Tokens.Jwt;

namespace Ecommerce_Application.Controllers
{
    public class AccountController : Controller
    {
        private IUserAuthentication authService;
        private UserManager<User> userManager;
        private IHttpContextAccessor httpContextAccessor;
        public AccountController(IUserAuthentication authService, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.authService = authService;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        /*        [Authorize(Roles ="Admin")]
        */
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Registration registration)
        {
            new Registration
            {
                Email = registration.Email,
                Username = registration.Username,
                Name = registration.Name,
                Password = registration.Password,
                PasswordConfirm = registration.PasswordConfirm,
                Role = registration.Role
            };
            var result = await authService.RegisterAsync(registration);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
           
            var userName = HttpContext.Request.Cookies["Username"];
            var password = HttpContext.Request.Cookies["Password"];
            
            ViewBag.Username = userName;
            ViewBag.Password = password;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var result = await authService.LoginAsync(login);
            if (result.StatusCode == 1)
            {
                var principal = httpContextAccessor.HttpContext.User;
                var UserId = userManager.GetUserId(principal);
                
                HttpContext.Session.SetString("UserId", UserId);
                HttpContext.Session.SetString("UserName", login.Username);

                CookieOptions cookies = new CookieOptions();
                cookies.Expires = DateTime.Now.AddMinutes(100);
                HttpContext.Response.Cookies.Append("Username", login.Username);
                HttpContext.Response.Cookies.Append("Password", login.Password);

                var token = await authService.GenerateToken(login);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                ViewBag.TokenString = tokenString;
                var obj = new { statusCode = result.StatusCode, message = result.Message, tokenString = tokenString };
                return Ok(obj);

            }
            return BadRequest();
        }

        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
