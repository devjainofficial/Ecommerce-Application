using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Application.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult UserInfo()
         {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = HttpContext.Request.Cookies["Username"];
            return View();
        }
    }
}





