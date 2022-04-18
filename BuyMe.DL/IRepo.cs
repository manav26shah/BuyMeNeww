using BuyMe.DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuyMe.DL
{
    public interface IRepo
    {
        List<Product> GetProducts();
    }
}
