﻿using MvcMovieStore.DataAccessLayer;
using MvcMovieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMovieStore.Repositories
{
    public class ShoppingCartRepository
    {
        ApplicationDbContext db = new ApplicationDbContext();

        string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        public static ShoppingCartRepository GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCartRepository();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        // Helper method to simplify shopping cart calls
        public static ShoppingCartRepository GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Album album, int quantity)
        {
            // Get the matching cart and album instances
            var cartItem = db.Carts.FirstOrDefault(
                c => c.CartId == ShoppingCartId
                && c.AlbumId == album.Id);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    AlbumId = album.Id,
                    CartId = ShoppingCartId,
                    Count = quantity,
                    DateCreated = DateTime.Now
                };

                db.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity
                cartItem.Count += quantity;
            }

            // Save changes
            db.SaveChanges();
        }

        public void UpdateCart(Album album, int quantity)
        {
            // Get the matching cart and album instances
            var cartItem = db.Carts.FirstOrDefault(
                c => c.CartId == ShoppingCartId
                && c.AlbumId == album.Id);

            cartItem.Count = quantity;
            db.Entry(cartItem).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            // Iterate over the items in the cart, adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    AlbumId = item.AlbumId,
                    OrderId = order.Id,
                    UnitPrice = item.Album.Price,
                    Quantity = item.Count
                };

                // Set the order total of the shopping cart
                orderTotal += (item.Count * item.Album.Price);

                db.OrderDetails.Add(orderDetail);

            }

            // Set the order's total to the orderTotal count
            order.Total = orderTotal;

            // Save the order
            db.SaveChanges();

            // Empty the shopping cart
            EmptyCart();

            // Return the OrderId as the confirmation number
            return order.Id;
        }

        public void CreateOrderDetails(Order order)
        {
            var cartItems = GetCartItems();

            // Iterate over the items in the cart, adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    AlbumId = item.AlbumId,
                    OrderId = order.Id,
                    UnitPrice = item.Album.Price,
                    Quantity = item.Count,
                    Order = db.Orders.Find(order.Id),
                    Album = db.Albums.Find(item.AlbumId)
                };

                db.OrderDetails.Add(orderDetail);

            }
            // Save the order
            db.SaveChanges();

            // Empty the shopping cart
            EmptyCart();
        }

        public void EmptyCart()
        {
            var cartItems = db.Carts.Where(cart => cart.CartId == ShoppingCartId).ToList();

            db.Carts.RemoveRange(cartItems);
            // Save changes
            db.SaveChanges();
        }

        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();

                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }

            return context.Session[CartSessionKey].ToString();
        }

        public List<Cart> GetCartItems()
        {
            return db.Carts.Where(cart => cart.CartId == ShoppingCartId
                ).ToList();
        }

        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in db.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();

            // Return 0 if all entries are null
            return count ?? 0;
        }

        public int GetNumberOfCartItems()
        {
            var cart = db.Carts.Where(c => c.CartId == ShoppingCartId
                ).ToList();

            return cart.Count;
        }

        public decimal GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in db.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count * cartItems.Album.Price).Sum();
            return total ?? decimal.Zero;
        }

        public void MigrateCart(string userName)
        {
            var shoppingCart = db.Carts.Where(c => c.CartId == ShoppingCartId
             );

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            db.SaveChanges();
        }

        public void RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = db.Carts.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.Id == id);

            db.Carts.Remove(cartItem);

            // Save changes
            db.SaveChanges();
        }
    }
}