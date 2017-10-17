using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedicalMarket.Data;
using MedicalMarket.Helper;
using MedicalMarket.ViewModels;
using System.Text.Encodings.Web;
using Microsoft.EntityFrameworkCore;

namespace MedicalMarket.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext _context;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /ShoppingCart/
        public IActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            var viewmodel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(viewmodel);
        }

        // GET: /Store/AddToCart/5
        public IActionResult AddToCart(AddToCartViewModel vm)
        {
            if (vm.Id ==null)
            {
                return NotFound();
            }

            var addedItem = _context.Items.SingleOrDefault(i => i.Id == vm.Id);

            if (addedItem == null)
            {
                return NotFound();
            }

            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addedItem, vm.Count);
            return StatusCode(200);
        }

        // AJAX: /ShoppingCart/RemoveFromCart/ID
        [HttpPost]
        public IActionResult RemoveFromCart(string id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var ItemName = _context.Carts.Include(c=> c.Item)
                                         .SingleOrDefault(i => i.RecordId == id)
                                         .Item.Name;

            var itemCount = cart.RemoveFromCart(id);

            var results = new ShoppingCartRemoveViewModel
            {
                Message = HtmlEncoder.Default.Encode(ItemName) +
                            " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Ok(results);
            //return Json(results);
        }

        // GET: /ShoppingCart/CartSummary
        //[ChildActionOnly]
        //public ActionResult CartSummary()
        //{
        //    var cart = ShoppingCart.GetCart(this.HttpContext);

        //    ViewData["CartCount"] = cart.GetCount();
        //    return PartialView("CartSummary");
        //}

    }
}