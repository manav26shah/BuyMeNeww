using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BuyMe.BL.Interface;
using BuyMe.DL;
using BuyMe.DL.Entities;
using BuyMe.DL.Repositories.Repo_Interfaces;

namespace BuyMe.BL.Implementation
{
    public class OrderService : IOrderService
    {
        private IRepo _repo;
        public OrderService(IRepo repo)
        {
            this._repo = repo;
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _repo.GetOrders();
        }

        public async Task<bool> PlaceOrder()
        {
            return await _repo.PlaceOrder();
        }
    }
}
