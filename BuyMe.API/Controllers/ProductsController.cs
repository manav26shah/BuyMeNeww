﻿using BuyMe.API.DTO.Request;
using BuyMe.API.DTO.Response;
using BuyMe.BL;
using BuyMe.BL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyMe.API.ExtensionMethods;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BuyMe.API.Controllers
{




    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        /// <summary>
        /// API to Get products (paginated)
        /// </summary>
        [HttpGet("{page}")]
        [Authorize]
        [ProducesResponseType(typeof(List<ProductResponse>), StatusCodes.Status200OK)]

        public IActionResult GetProducts(int page)
        {

            /* var nameOFShip = "Maersk India * * Container";
             nameOFShip.CountStars();*/
            var pageResult = 1f;

            var dbProducts = _productService.GetProducts(page, pageResult);
            var retProducts = new List<ProductResponse>();


            foreach (var item in dbProducts)
            {
                retProducts.Add(new ProductResponse
                {
                    Id = item.Id,
                    Name = item.Name,
                    Image = item.Image,
                    MRPAmount = item.MRPAmount,
                    Discount = item.DiscountPercentage,
                    InStock = item.InStock
                });
            }  // Auto mapper

            var pageCount = Math.Ceiling(retProducts.Count() / pageResult);
            var response = new PageResponse
            {
                Products = retProducts,
                CurrrentPage = page,
                Pages = (int)pageCount
            };
            return Ok(response);
        }

        /// <summary>
        /// API to Get products based on Category ID
        /// </summary>
        [HttpGet("category/{id}/{page}")]
        [Authorize]
        [ProducesResponseType(typeof(List<ProductResponse>), StatusCodes.Status200OK)]
        public IActionResult GetProductbyCId([FromRoute] int id, int page)
        {
            var pageResult = 1f;
            var dbProducts = _productService.GetProductsByCId(id, page, pageResult);
            var retProducts = new List<ProductResponse>();


            foreach (var item in dbProducts)
            {
                retProducts.Add(new ProductResponse
                {
                    Id = item.Id,
                    Name = item.Name,
                    Image = item.Image,
                    MRPAmount = item.MRPAmount,
                    Discount = item.DiscountPercentage,
                    InStock = item.InStock
                });
            }  // Auto mapper

            var pageCount = Math.Ceiling(retProducts.Count() / pageResult);
            var response = new PageResponse
            {
                Products = retProducts,
                CurrrentPage = page,
                Pages = (int)pageCount
            };
            return Ok(response);
        }

        /// <summary>
        /// API to add a new product
        /// </summary>
        /// <param name="data">data required to add new product</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> AddNewProduct([FromBody] ProductRequest data)
        {

            try
            {
                var headers = HttpContext.Request.Headers.ToList();

                var newProductBl = new ProductBL
                {
                    Name = data.Name,
                    MRPAmount = (decimal)data.MRP,
                    CategoryId = (int)data.CategoryId,
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
                return StatusCode(StatusCodes.Status500InternalServerError);

            }


        }

        /*[HttpPut]
        public IActionResult UpdateExistingProduct()
        {
            return Ok("THis API is not complete");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            return Ok();
        }
*/

        /// <summary>
        /// API to Search Product (partial match)
        /// </summary>
        [HttpGet("search/{exp}")]
        [Authorize]
        public IActionResult Search([FromRoute] string exp)
        {
            var dbProducts = _productService.GetProductByMatch(exp);
            var retProducts = new List<ProductResponse>();

            foreach (var item in dbProducts)
            {
                retProducts.Add(new ProductResponse
                {
                    Id = item.Id,
                    Name = item.Name,
                    Image = item.Image,
                    MRPAmount = item.MRPAmount,
                    Discount = item.DiscountPercentage,
                    InStock = item.InStock
                });
            }  // Auto mapper
            return Ok(retProducts);
        }

        [HttpGet("GetProducts")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public IActionResult GetProducts()
        {
            var dbProducts = _productService.GetProducts();
            var retProducts = new List<ProductResponse>();

            foreach (var item in dbProducts)
            {
                retProducts.Add(new ProductResponse
                {
                    Id = item.Id,
                    Name = item.Name,
                    Image = item.Image,
                    MRPAmount = item.MRPAmount,
                    Discount = item.DiscountPercentage,
                    InStock = item.InStock
                });
            }  // Auto mapper

            var res = new Response<List<ProductResponse>>
            {
                Data = retProducts
            };
            return Ok(res);
        }
    }
}
