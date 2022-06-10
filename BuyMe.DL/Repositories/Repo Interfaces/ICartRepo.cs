using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BuyMe.DL.Entities;
using BuyMe.DL.Models;

namespace BuyMe.DL.Repositories
{
    public interface ICartRepo
    {
        Task<bool> AddItem(Cart cart);
        Task<bool> UpdateItem(Cart cart);
        Task<bool> DeleteItem(Cart cart);
        Task<List<CartItemModel>> GetCartItems();

    }
}
