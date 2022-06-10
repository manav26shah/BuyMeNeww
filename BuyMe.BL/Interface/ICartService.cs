using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BuyMe.BL.Models;
using BuyMe.DL.Entities;

namespace BuyMe.BL.Interface
{
    public interface ICartService
    {
        Task<bool> AddItem(CartItemBL cartItem);
        Task<bool> UpdateItem(CartItemBL cartItem);
        Task<bool> DeleteItem(CartItemBL cartItem);
        Task<CartBL> GetCartItems();
    }
}
