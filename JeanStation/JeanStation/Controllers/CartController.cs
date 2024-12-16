using JeanStation.Models;
using JeanStation.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JeanStation.Entities;

namespace JeanStation.Controllers
{
    [RoutePrefix("api/cart")]
    public class CartController : ApiController
    {
        private readonly ICartRepository _cartRepository;

        public CartController()
        {
            _cartRepository = new CartRepository();
        }

        // Adds a new item to the cart
        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddToCart(Cartdto cartDto)
        {
            if (cartDto == null)
                return BadRequest("Cart data cannot be null.");

            try
            {
                _cartRepository.AddToCart(cartDto);
                return Ok("Item added to cart successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Retrieves all cart items for a specific customer
        [HttpGet]
        [Route("items/{customerId}")]
        public IHttpActionResult GetCartItems(string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
                return BadRequest("Customer ID cannot be null or empty.");

            try
            {
                var cartItems = _cartRepository.GetCartItems(customerId);
                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Removes an item from the cart
        [HttpDelete]
        [Route("remove/{cartId}")]
        public IHttpActionResult RemoveCartItem(string cartId)
        {
            if (string.IsNullOrWhiteSpace(cartId))
                return BadRequest("Cart ID cannot be null or empty.");

            try
            {
                _cartRepository.RemoveCartItem(cartId);
                return Ok("Item removed from cart successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Updates an existing cart item
        [HttpPut]
        [Route("update")]
        public IHttpActionResult EditCartItem(Cartdto cartDto)
        {
            if (cartDto == null)
                return BadRequest("Cart data cannot be null.");

            try
            {
                _cartRepository.EditCartItem(cartDto);
                return Ok("Cart item updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Clears all items from the cart
        [HttpDelete]
        [Route("clear/{customerId}")]
        public IHttpActionResult ClearCart(string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
                return BadRequest("Customer ID cannot be null or empty.");

            try
            {
                _cartRepository.ClearCart(customerId);
                return Ok("Cart cleared successfully for the customer.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

