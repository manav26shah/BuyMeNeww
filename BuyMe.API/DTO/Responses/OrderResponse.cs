using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyMe.API.Models;

namespace BuyMe.API.DTO.Responses
{
    public class OrderResponse
    {
        public List<OrderItemModel> orderItems { get; set; } = new List<OrderItemModel>();
    }
}
