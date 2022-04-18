using BuyMe.BL.Interface;
using BuyMe.DL;
using BuyMe.DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuyMe.BL.Implementation
{
    public class ProductService : IProductService
    {
        private IRepo _repo;

        public ProductService(IRepo repo)
        {
            _repo = repo;
        }
        public List<Product> GetProducts()
        {
            return _repo.GetProducts();
        }
    }
}
