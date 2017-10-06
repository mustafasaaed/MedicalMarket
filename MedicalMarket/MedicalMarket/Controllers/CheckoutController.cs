using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MedicalMarket.Data;
using Microsoft.AspNetCore.Http;
using MedicalMarket.Models.App;
using MedicalMarket.Helper;

namespace MedicalMarket.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CheckoutController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public ActionResult AddressAndPayment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddressAndPayment(IFormCollection values)
        {
            var order = new Order();
            TryUpdateModelAsync(order);

            try
            {
                order.UserName = User.Identity.Name;
                order.OrderDate = DateTime.Now;

                //Save Order
                _context.Orders.Add(order);
                _context.SaveChanges();
                //Process the order
                var cart = ShoppingCart.GetCart(this.HttpContext);
                cart.CreateOrder(order);

                return RedirectToAction("Complete",
                    new { id = order.Id });

            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }

        //
        // GET: /Checkout/Complete
        public ActionResult Complete(string id)
        {
            // Validate customer owns this order
            bool isValid = _context.Orders.Any(
                o => o.Id == id &&
                o.UserName == User.Identity.Name);

            if (isValid)
            {
                return View("Complete", id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}