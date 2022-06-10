using System;
using System.Collections.Generic;
using System.Text;
using BuyMe.DL.Entities;

namespace BuyMe.DL.Models
{
    public class CartItemModel
    {
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}
