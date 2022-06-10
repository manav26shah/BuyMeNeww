using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BuyMe.DL.Entities;

namespace BuyMe.BL.Interface
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts(int limit=10, int page=1);
        Task<List<Product>> GetProductsByCategory(int CategoryId, int limit = 10, int page = 1);
        Task<List<Product>> GetProductsByName(string name, int limit = 10, int page = 1);
        Task<bool> AddNewProduct(ProductBL newProduct);
    }
}
