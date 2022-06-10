using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BuyMe.BL.Interface;
using BuyMe.DL;
using BuyMe.DL.Entities;
using BuyMe.DL.Repositories;

namespace BuyMe.BL.Implementation
{
    public class ProductService : IProductService
    {
        private IRepo _repo;

        public ProductService(IRepo repo)
        {
            this._repo = repo;
        }

        public async Task<bool> AddNewProduct(ProductBL newProduct)
        {
            if (DateTime.UtcNow.Month == 9 || DateTime.UtcNow.Month == 10)
            {
                newProduct.MaxOrderAmount = 2;
            }
            var newEntity = new Product
            {
                CategoryId = newProduct.CategoryId,
                Name = newProduct.Name,
                MRPAmount = newProduct.MRPAmount,
                InStock = newProduct.InStock,
                MaxOrderAmount = newProduct.MaxOrderAmount
            };
            return await _repo.AddNewProduct(newEntity);
        }

        public async Task<List<Product>> GetProducts(int limit = 10, int page = 1)
        {
            return await _repo.GetProducts(limit, page);
        }

        public async Task<List<Product>> GetProductsByCategory(int CategoryId, int limit = 10, int page = 1)
        {
            return await _repo.GetProductsByCategory(CategoryId, limit, page);
        }

        public async Task<List<Product>> GetProductsByName(string name, int limit = 10, int page = 1)
        {
            return await _repo.GetProductsByName(name);
        }
    }
}
