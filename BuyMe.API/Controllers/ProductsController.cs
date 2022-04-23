using BuyMe.API.DTO.Request;
using BuyMe.API.DTO.Response;
using BuyMe.BL;
using BuyMe.BL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyMe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // big amount of data 10k products
        [HttpGet]
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
            return Ok(retProducts);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductbyId([FromRoute] int id)
        {
            return Ok();
        }

        /// <summary>
        /// API to add a new product
        /// </summary>
        /// <param name="data">data required to add new product</param>
        /// <returns></returns>
        [HttpPost]
       
        public async Task<IActionResult> AddNewProduct([FromBody] ProductRequest data)
        {
            var newProductBl = new ProductBL
            {
                Name = data.Name,
                MRPAmount = data.MRP,
                CategoryId = data.CategoryId,
                MaxOrderAmount = data.MaxOrderAmount
            };
            var result = await _productService.AddNewProduct(newProductBl);
            if (result)
            {
                return Ok("Product added successfully");
            }
            else
            {
                return BadRequest("Error while adding new product");
            }
        }

        [HttpPut]
       
        public IActionResult UpdateExistingProduct()
        {
            return Ok("THis API is not complete");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            return Ok();
        }
        [HttpPost("search")]
        public IActionResult Search()
        {
            return Ok();
        }

        // you cannot return a response body , you can only return response headers
        [HttpHead]
        public IActionResult TestAPi()
        {
            // do some calculation , query db etc for count etc
            HttpContext.Response.Headers.Add("response-size", "100");
            return Ok();
        }
    }  
}
