﻿using JeanStation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JeanStation.Models;
namespace JeanStation.Repository
{
    public class CartRepository : ICartRepository
    {

        private readonly JeanStationContext _context;

        public CartRepository()
        {
            _context = new JeanStationContext();
        }

        // Adds a new cart item to the database
        public void AddToCart(Cartdto cart)
        {
            if (cart == null)
                throw new ArgumentNullException(nameof(cart));

            var cartEntity = new Cart
            {
                CartId = cart.CartId,
                JeansId = cart.JeansId,
                Price = cart.Price,
                CustomerId = cart.CustomerId,
                Quantity = cart.Quantity
            };

            _context.Carts.Add(cartEntity);
            _context.SaveChanges();
        }


        // Retrieves all cart items for a specific customer
        public List<Cart> GetCartItems(string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
                throw new ArgumentException("Customer ID cannot be null or empty", nameof(customerId));

            return _context.Carts.Where(c => c.CustomerId == customerId).ToList();

        }

        // Removes a specific cart item based on CartId
        public void RemoveCartItem(string cartId)
        {
            if (string.IsNullOrWhiteSpace(cartId))
                throw new ArgumentException("Cart ID cannot be null or empty", nameof(cartId));

            var cartItem = _context.Carts.FirstOrDefault(c => c.CartId == cartId);
            if (cartItem != null)
            {
                _context.Carts.Remove(cartItem);
                _context.SaveChanges();
            }
        }

        // Edits/updates an existing cart item
        public void EditCartItem(Cartdto cart)
        {
            if (cart == null)
                throw new ArgumentNullException(nameof(cart));

            var existingCart = _context.Carts.FirstOrDefault(c => c.CartId == cart.CartId);
            if (existingCart != null)
            {
                //existingCart.JeansId = cart.JeansId;
                existingCart.Price = cart.Price;
                existingCart.Quantity = cart.Quantity;
                _context.SaveChanges();
            }
        }

        // Clears all items from the cart (for all customers or based on additional criteria if required)
        public void ClearCart(string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
                throw new ArgumentException("Customer ID cannot be null or empty", nameof(customerId));

            var customerCartItems = _context.Carts.Where(c => c.CustomerId == customerId).ToList();
            if (customerCartItems.Any())
            {
                _context.Carts.RemoveRange(customerCartItems);
                _context.SaveChanges();
            }
        }
    }
}
