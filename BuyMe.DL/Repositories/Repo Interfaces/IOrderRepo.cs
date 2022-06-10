﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BuyMe.DL.Entities;

namespace BuyMe.DL.Repositories.Repo_Interfaces
{
    public interface IOrderRepo
    {
        Task<List<Order>> GetOrders();
        Task<bool> PlaceOrder();
    }
}
