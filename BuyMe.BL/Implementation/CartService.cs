using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BuyMe.BL.Interface;
using BuyMe.BL.Models;
using BuyMe.DL;
using BuyMe.DL.Entities;

namespace BuyMe.BL.Implementation
{
    public class CartService : ICartService
    {
        private IRepo _repo;

        public CartService(IRepo repo)
        {
            this._repo = repo;
        }

        public async Task<bool> AddItem(CartItemBL cartItem)
        {
            return await _repo.AddItem(new Cart{ ProductId = cartItem.ProductId, Count = cartItem.Count });
        }

        public async Task<bool> DeleteItem(CartItemBL cartItem)
        {
            return await _repo.DeleteItem(new Cart { ProductId = cartItem.ProductId});
        }

        public async Task<CartBL> GetCartItems()
        {
            var products = await _repo.GetCartItems();
            //double totalAmount = 0;
            //int numberOfProducts = 0;
            var cart = new CartBL();
            foreach (var product in products)
            {
                var cartItem = new CartItemBL
                {
                    ProductId = product.Product.Id,
                    Name = product.Product.Name,
                    Image = product.Product.Image,
                    MRPAmount = product.Product.MRPAmount,
                    Count = product.Count
                };
                cart.CartItems.Add(cartItem);
                cart.TotalAmount += product.Product.MRPAmount * product.Count;
                cart.NumberOfProducts += product.Count;
            }

            return cart;
        }

        public async Task<bool> UpdateItem(CartItemBL cartItem)
        {
            return await _repo.UpdateItem(new Cart { ProductId = cartItem.ProductId, Count = cartItem.Count });
        }
    }
}
