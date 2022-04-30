using BuyMe.API.DTO.Request;
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

        // big amount of data 10k products
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof (Response<List<ProductResponse>>),StatusCodes.Status200OK)]
        public IActionResult GetProducts()
        {

            var nameOFShip = "Maersk India * * Container";
            nameOFShip.CountStars();
            
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

            var res = new Response<List<ProductResponse>>();
            res.Data = retProducts;
            return Ok(res);
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewProduct([FromBody] ProductRequest data)
        {

            try
            {
                var headers = HttpContext.Request.Headers.ToList();
                
                var newProductBl = new ProductBL
                {
                    Name = data.Name,
                    MRPAmount = data.MRP,
                    CategoryId = data.CategoryId,
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
                   
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
               
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
