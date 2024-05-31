using Ecommerce_Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using System.Diagnostics;

namespace Ecommerce_Application.Controllers
{
    public class StripeSettingsController : Controller
    {
        private readonly StripeSettings _stripeSettings;
        public StripeSettingsController(IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
          
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PlaceOrder(int Amount)
        {
            ViewBag.Amount = Amount;
            return View();
        }
        public IActionResult CreateCheckoutSession(string amount)
        {
            var currency = "inr";
            var successUrl = "https://localhost:7210/StripeSettings/Success";
            var cancelUrl = "https://localhost:7210/Home/Cancel";
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = currency,
                            UnitAmount = Convert.ToInt32(amount) * 100,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Payment Amount"
                                
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl
            };

            var service = new SessionService();
            var session = service.Create(options);
            return Redirect(session.Url);
        }

        public async Task<IActionResult> Success()
        {

            return RedirectToAction("OrderSuccess", "Cart");
        }

        public IActionResult cancel()
        {
            return View("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
