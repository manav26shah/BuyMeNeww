using BuyMe.DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace BuyMe.DL
{
    public class Repo:IRepo
    {
        private AppDbContext _dbContext;

        public Repo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async  Task<bool> AddNewProduct(Product newProduct)
        {
            _dbContext.Products.Add(newProduct);
            var result = await _dbContext.SaveChangesAsync();
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public List<Product> GetProducts()
        {
            return _dbContext.Products.ToList();
        }
    }
}
