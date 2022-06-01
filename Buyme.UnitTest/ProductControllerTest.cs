/*using Buyme.UnitTest.Mocks;
using BuyMe.API.Controllers;
using BuyMe.API.DTO.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Xunit;

namespace Buyme.UnitTest
{
    public class ProductControllerTest
    {
       
       

        [Fact]  // signifies the test runner , this is a test method and it has to be run
        public void AddNewProduct_WhenCalled_ReturnsCreatedResult()
        {
            // AAA
            // Arrange Act Assert

            // Arrange

            var productSevice = new ProductServiceMock();
            var logFactory = LoggerFactory.Create(builder=>builder.AddConsole());
            var logger = logFactory.CreateLogger<ProductsController>();
            var productController = new ProductsController(productSevice, logger);
            var reqData = new ProductRequest()
            {
                Name = "Unit test product",
                CategoryId=0,
                MRP=45
            };

            // Act
            var result = productController.AddNewProduct(reqData).GetAwaiter().GetResult();
            var statusCodeResult = result as StatusCodeResult;

            // Assert
            Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(StatusCodes.Status201Created,statusCodeResult.StatusCode);
        }

        [Fact]
        public void AddNewProduct_WhenCalled_ReturnsBadRequest()
        {
            // AAA
            // Arrange Act Assert

            // Arrange

            var productSevice = new ProductServiceMock();
            var logFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = logFactory.CreateLogger<ProductsController>();
            var productController = new ProductsController(productSevice, logger);
            var reqData = new ProductRequest();

            // Act
            var result = productController.AddNewProduct(reqData).GetAwaiter().GetResult();
            var statusCodeResult = result as BadRequestResult;

            // Assert
            Assert.IsType<BadRequestResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, statusCodeResult.StatusCode);

            var reqData1 = new ProductRequest()
            {
                Name = "Unit test product",
                CategoryId = 0,
                MRP = 45
            };
            // Act
            var result1 = productController.AddNewProduct(reqData1).GetAwaiter().GetResult();
            var statusCodeResult1 = result1 as BadRequestResult;

            // Assert
            Assert.IsType<BadRequestResult>(result1);
            Assert.Equal(StatusCodes.Status400BadRequest, statusCodeResult1.StatusCode);

            var reqData2 = new ProductRequest()
            {
                Name = "Unit test product",
                CategoryId = 0
            };
            // Act
            var result2 = productController.AddNewProduct(reqData2).GetAwaiter().GetResult();
            var statusCodeResult2 = result1 as BadRequestResult;

            // Assert
            Assert.IsType<BadRequestResult>(result2);
            Assert.Equal(StatusCodes.Status400BadRequest, statusCodeResult2.StatusCode);
        }
    }
}
*/