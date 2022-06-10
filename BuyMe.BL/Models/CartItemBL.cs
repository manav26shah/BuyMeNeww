using System;
using System.Collections.Generic;
using System.Text;

namespace BuyMe.BL
{
    public class CartItemBL
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double MRPAmount { get; set; }
        public int Count { get; set; }
    }
}
