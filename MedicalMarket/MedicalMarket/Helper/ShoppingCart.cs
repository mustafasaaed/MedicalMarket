using MedicalMarket.Data;
using MedicalMarket.Models.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalMarket.Helper
{
    public class ShoppingCart
    {
        private readonly ApplicationDbContext _context;
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";

        public ShoppingCart(ApplicationDbContext context)
        {
            this._context = context;
        }
        public ShoppingCart()
        {
        }

        public static ShoppingCart GetCart(HttpContext context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Item item)
        {
            var cartItem = _context.Carts
                .SingleOrDefault(c => c.CartId == ShoppingCartId
                                 && c.Item.Id == item.Id);

            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    Count = 1,
                    CreatedAt = DateTime.UtcNow,
                    CartId = ShoppingCartId,
                    ItemId = item.Id
                };
                _context.Carts.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }
            _context.SaveChanges();
        }

        public int RemoveFromCart(string id)
        {
            var cartItem = _context.Carts
                        .Single(cart => cart.CartId == ShoppingCartId
                        && cart.RecodId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    _context.Carts.Remove(cartItem);
                }
                _context.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = _context.Carts.Where(c => c.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                _context.Carts.Remove(cartItem);
            }
            _context.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return _context.Carts.Where(c => c.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            int? count = (from cartItems in _context.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();

            return count ?? 0;
        }

        public decimal GetTotal()
        {
            decimal? total = (from cartItems in _context.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.Item.Price).Sum();

            return total ?? decimal.Zero;
        }

        public string CreateOrder(Order order)
        {
            decimal orderTotal = 0;
            var cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ItemId = item.ItemId,
                    OrderId = order.Id,
                    UnitPrice = item.Item.Price,
                    Quantity = item.Count
                };

                orderTotal += (item.Count * item.Item.Price);
                _context.OrderDetails.Add(orderDetail);
            }
            order.Total = orderTotal;
            _context.SaveChanges();
            EmptyCart();
            return order.Id;
        }

        public string GetCartId(HttpContext context)
        {
            if (context.Session.GetString(CartSessionKey) == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session.SetString(CartSessionKey, 
                        context.User.Identity.Name);
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session.SetString(CartSessionKey,tempCartId.ToString());
                }
            }
            return context.Session.GetString(CartSessionKey.ToString());
        }

        public void MigrateCart(string userName)
        {
            var shoppingCart = _context.Carts.Where(
                c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            _context.SaveChanges();
        }


    }
}
