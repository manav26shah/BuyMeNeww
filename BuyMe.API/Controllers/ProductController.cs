using BuyMe.API.DTO.Response;
using BuyMe.BL.Interface;
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
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var dbProducts = _productService.GetProducts();
            var retProducts = new List<ProductResponse>();

            foreach (var item in dbProducts)
            {
                retProducts.Add(new ProductResponse
                {
                    Id=item.Id,
                    Name=item.Name,
                    Image=item.Image,
                    MRPAmount=item.MRPAmount,
                    Discount=item.DiscountPercentage,
                    InStock=item.InStock
                });
            }  // Auto mapper
            return Ok(retProducts);
        }
    }
}
