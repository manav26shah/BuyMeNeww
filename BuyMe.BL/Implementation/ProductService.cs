﻿using BuyMe.BL.Interface;
using BuyMe.DL;
using BuyMe.DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuyMe.BL.Implementation
{
    public class ProductService : IProductService
    {
        private IRepo _repo;

        public ProductService(IRepo repo)
        {
            _repo = repo;
        }

       
        public async Task<bool> AddNewProduct(ProductBL newProduct)
        {
            if(DateTime.UtcNow.Month==9 || DateTime.UtcNow.Month == 10)
            {
                newProduct.MaxOrderAmount = 2;
            }
            var newEntity = new Product
            {
                CategoryId = newProduct.CategoryId,
                Name = newProduct.Name,
                MaxOrderAmount=newProduct.MaxOrderAmount
            };
            return await _repo.AddNewProduct(newEntity);
        }

        public List<Product> GetProducts()
        {
            return _repo.GetProducts();
        }
    }
}