using BuyMe.DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuyMe.DL
{
    public interface IRepo
    {
        List<Product> GetProducts();
        Task<bool> AddNewProduct(Product newProduct);
    }
}
