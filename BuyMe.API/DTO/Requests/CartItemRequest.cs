using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyMe.API.DTO.Requests
{
    public class CartItemRequest
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
