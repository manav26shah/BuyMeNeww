using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BuyMe.DL.Entities;

namespace BuyMe.BL.Interface
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrders();
        Task<bool> PlaceOrder();
    }
}
