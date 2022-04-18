using BuyMe.DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BuyMe.DL
{
    public class Repo:IRepo
    {
        private AppDbContext _dbContext;

        public Repo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Product> GetProducts()
        {
            return _dbContext.Products.ToList();
        }
    }
}
