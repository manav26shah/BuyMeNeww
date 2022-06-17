using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyMe.API.DTO;
using BuyMe.API.DTO.Requests;
using BuyMe.API.DTO.Responses;
using BuyMe.API.Models;
using BuyMe.BL;
using BuyMe.BL.Interface;
using System.Text.RegularExpressions;

namespace BuyMe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        private ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        /// <summary>
        /// Get all Products
        /// </summary>
        /// <remarks>
        /// Query Parameters
        /// 
        /// </remarks>
        /// <param name="limit">Number of items per page</param>
        /// <param name="page">Current page number</param>
        [HttpGet]
        public async Task<ActionResult> GetProducts([FromQuery] int limit=10, int page=1)
        {
            try
            {
                var numberOfItemsPerPage = limit > 20 ? 20 : limit;
                var dbProducts = await _productService.GetProducts(numberOfItemsPerPage, page);
                var retProducts = new List<ProductModel>();
                _logger.LogTrace(limit.ToString());
                var baseUrl = "https://localhost:44308/images/";
                foreach (var item in dbProducts)
                {
                    retProducts.Add(new ProductModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Image = baseUrl + item.Image,
                        MRPAmount = item.MRPAmount,
                        Discount = item.DiscountPercentage,
                        InStock = item.InStock,
                        MaxOrderQuantity = item.MaxOrderAmount
                    });
                }
                return StatusCode(StatusCodes.Status200OK, new ProductResponse { Products = retProducts, CurrentPage = page });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                var res = new Response();
                res.Message.Add("Some Error Occurred!");
                return StatusCode(StatusCodes.Status500InternalServerError, res);
            }
            
        }

        /// <summary>
        /// Get products by category(id)
        /// </summary>
        /// <remarks>
        /// Query Parameters
        /// 
        /// </remarks>
        /// <param name="limit">Number of items per page</param>
        /// <param name="page">Current page number</param>
        [HttpGet("category/{id}")]
        public async Task<ActionResult> GetProductbyCategory([FromRoute] int id, [FromQuery] int limit = 10, int page = 1)
        {
            try
            {
                var numberOfItemsPerPage = limit > 20 ? 20 : limit;
                var dbProducts = await _productService.GetProductsByCategory(id, numberOfItemsPerPage, page);
                var retProducts = new List<ProductModel>();
                foreach (var item in dbProducts)
                {
                    retProducts.Add(new ProductModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Image = item.Image,
                        MRPAmount = item.MRPAmount,
                        Discount = item.DiscountPercentage,
                        InStock = item.InStock
                    });
                }  // Auto mapper
                return StatusCode(StatusCodes.Status200OK, new ProductResponse { Products = retProducts, CurrentPage = page });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                var res = new Response();
                res.Message.Add("Some Error Occurred!");
                return StatusCode(StatusCodes.Status500InternalServerError, res);
            }
            
        }

        /// <summary>
        /// Search product by name
        /// </summary>        
        /// <remarks>
        /// Query Parameters
        /// 
        /// </remarks>
        
        [HttpGet("search/{exp}")]
        //[Authorize]
        public async Task<ActionResult> GetProductbyName([FromRoute] string exp)
        {
            try
            {
                var dbProducts = await _productService.GetProductsByName(exp);
                var retProducts = new List<ProductModel>();
                Console.WriteLine(HttpContext.User.Identity.Name);
                foreach (var item in dbProducts)
                {
                    retProducts.Add(new ProductModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Image = item.Image,
                        MRPAmount = item.MRPAmount,
                        Discount = item.DiscountPercentage,
                        InStock = item.InStock
                    });
                }  // Auto mapper
                return StatusCode(StatusCodes.Status200OK, new ProductResponse { Products = retProducts, CurrentPage = 1 });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                var res = new Response();
                res.Message.Add("Some Error Occurred!");
                return StatusCode(StatusCodes.Status500InternalServerError, res);
            }
            
        }


        /// <summary>
        /// Add a new product
        /// </summary>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddNewProduct([FromBody] ProductRequest data)
        {

            try
            {
                var newProductBl = new ProductBL
                {
                    Name = data.Name,
                    MRPAmount = data.MRP,
                    CategoryId = data.CategoryId,
                    InStock = data.InStock,
                    MaxOrderAmount = data.MaxOrderAmount
                };
                var result = await _productService.AddNewProduct(newProductBl);
                _logger.LogTrace("Connected and sent data to the DB correctly");
                if (result)
                {
                    return StatusCode(StatusCodes.Status201Created);

                }
                else
                {
                    return BadRequest("Error while adding new product");
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                var res = new Response();
                res.Message.Add("Some Error Occurred!");
                return StatusCode(StatusCodes.Status500InternalServerError, res);
            }
        }

        /// <summary>
        /// Check product availability by pincode 
        /// </summary>
        /// <remarks>
        [HttpPost("{pincode}")]
        public async Task<IActionResult> CheckAvailability([FromRoute] string pincode, int productId)
        {
            try
            {

                Regex rgx = new Regex("^[1-9][0-9]{5}$");
                var res = rgx.IsMatch(pincode);
                if (res)
                {
                    var result = await _productService.CheckAvailability(pincode, productId);
                    if(result.Id != "0")
                        return StatusCode(StatusCodes.Status200OK, result);
                    else
                        return BadRequest(result);
                }
                else
                    return BadRequest("Error while validating pincode");
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
