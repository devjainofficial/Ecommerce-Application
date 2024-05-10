using Ecommerce_Application.Models;
using Ecommerce_Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.IdentityModel.Tokens.Jwt;

namespace Ecommerce_Application.Controllers
{
    public class AccountController : Controller
    {
        private IUserAuthentication authService;
        public AccountController(IUserAuthentication authService)
        {
            this.authService = authService;
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
            return Ok(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
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
                var token = await authService.GenerateToken(login);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                ViewBag.TokenString = tokenString;
                return Ok(tokenString);

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
