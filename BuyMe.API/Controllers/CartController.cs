using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BuyMe.API.DTO;
using BuyMe.API.DTO.Requests;
using BuyMe.API.DTO.Responses;
using BuyMe.API.Models;
using BuyMe.BL;
using BuyMe.BL.Interface;
using BuyMe.BL.Models;

namespace BuyMe.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private ILogger<CartController> _logger;
        private ICartService _cartService;
        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            this._logger = logger;
            this._cartService = cartService;
        }

        /// <summary>
        /// Get items in cart
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetCartItems()
        {
            try
            {
                var cartResponse = new CartResponse();
                var cart = await _cartService.GetCartItems();
                if(cart.NumberOfProducts == 0)
                {
                    var res = new Response<CartBL>();
                    var cartBL = new CartBL();
                    cartBL.NumberOfProducts = 0;
                    cartBL.TotalAmount = 0;
                    res.Data = cartBL;
                    res.Message.Add("No items in your cart.");
                    return Ok(res);
                }
                return Ok(new Response<CartBL> { Data = cart});
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                var res = new Response();
                res.Message.Add($"Some Error Occurred: {ex.ToString()}");
                return StatusCode(StatusCodes.Status500InternalServerError, res);
            }
        }

        /// <summary>
        /// Add item to cart
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartItemRequest cartItem)
        {
            try
            {
                var result = await _cartService.AddItem(new CartItemBL { ProductId = cartItem.ProductId, Count = cartItem.Count });
                if(result == true)
                {
                    var res = new Response();
                    res.Message.Add("Item Added Successfully to the Cart");
                    return StatusCode(StatusCodes.Status200OK, res);
                }
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                var res = new Response();
                res.Message.Add("Some Error Occurred!");
                return StatusCode(StatusCodes.Status500InternalServerError, res);
            }
        }

        /// <summary>
        /// Update item in cart
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateCart([FromBody] CartItemRequest cartItem)
        {
            try
            {
                var result = await _cartService.UpdateItem(new CartItemBL { ProductId = cartItem.ProductId, Count = cartItem.Count });
                if (result == true)
                {
                    var res = new Response();
                    res.Message.Add("Item updated in cart.");
                    return StatusCode(StatusCodes.Status200OK, res);
                }
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                var res = new Response();
                res.Message.Add("Some Error Occurred!");
                return StatusCode(StatusCodes.Status500InternalServerError, res);
            }
        }

        /// <summary>
        /// Delete item from cart
        /// </summary>
        /// <remarks>
        /// 
        /// Route -> /api/cart/{product-id}
        /// 
        /// </remarks>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCartItem([FromRoute] int Id)
        {
            try
            {
                var result = await _cartService.DeleteItem(new CartItemBL { ProductId = Id });
                if (result == true)
                {
                    var res = new Response();
                    res.Message.Add("Item deleted from cart successfully.");
                    return StatusCode(StatusCodes.Status200OK, res);
                }
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                var res = new Response();
                res.Message.Add("Some Error Occurred!");
                return StatusCode(StatusCodes.Status500InternalServerError, res);
            }
        }
    }
}
