using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BuyMe.DL.Entities;
using BuyMe.DL.Models;

namespace BuyMe.DL
{
    public interface IRepo
    {
        Task<List<Product>> GetProducts(int limit = 10, int page = 1);
        Task<List<Product>> GetProductsByCategory(int categoryId, int limit = 10, int page = 1);
        Task<List<Product>> GetProductsByName(string name);
        Task<bool> AddNewProduct(Product newProduct);


        Task<List<Order>> GetOrders();
        Task<bool> PlaceOrder();

        Task<bool> AddItem(Cart cart);
        Task<bool> UpdateItem(Cart cart);
        Task<bool> DeleteItem(Cart cart);
        Task<List<CartItemModel>> GetCartItems();

    }
}