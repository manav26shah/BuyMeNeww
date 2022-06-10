using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyMe.API.Models;
using BuyMe.BL;

namespace BuyMe.API.DTO.Responses
{
    public class CartResponse
    {
        public List<CartItemBL> CartItem { get; set; } = new List<CartItemBL>();
        public int NumberOfProducts { get; set; }
        public int TotalAmount { get; set; }
    }
}
