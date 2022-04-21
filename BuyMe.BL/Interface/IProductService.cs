using BuyMe.DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuyMe.BL.Interface
{
    public interface IProductService
    {
        public List<Product> GetProducts();
        public Task<bool> AddNewProduct(ProductBL newProduct);
    }
}
