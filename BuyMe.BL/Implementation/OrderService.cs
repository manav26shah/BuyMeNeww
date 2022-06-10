using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BuyMe.BL.Interface;
using BuyMe.DL.Entities;
using BuyMe.DL.Repositories.Repo_Interfaces;

namespace BuyMe.BL.Implementation
{
    public class OrderService : IOrderService
    {
        private IOrderRepo _orderRepo;
        public OrderService(IOrderRepo orderRepo)
        {
            this._orderRepo = orderRepo;
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _orderRepo.GetOrders();
        }

        public async Task<bool> PlaceOrder()
        {
            return await _orderRepo.PlaceOrder();
        }
    }
}
