using BuyMe.DL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuyMe.DL
{
    public class AppDbContext :DbContext // this inherits from DbContext
    {
        
        public DbSet<Product> Products { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);
            builder.Entity<Product>().HasData(
                new Product
                {
                    Id=1,
                    Name="Oppo Reno 6",
                    Image="oppo1.png",
                    MRPAmount=15999,
                    DiscountPercentage=10,
                    InStock=true,
                    MaxOrderAmount=3,
                    InventoryId="Mob-oppo-1"
                },
                new Product
                {
                    Id=2,
                    Name = "Vivo X",
                    Image = "vivo1.png",
                    MRPAmount = 15999,
                    DiscountPercentage = 10,
                    InStock = true,
                    MaxOrderAmount = 3,
                    InventoryId = "Mob-vivo-1"
                },
                new Product
                {
                    Id=3,
                    Name = "Samsung M31",
                    Image = "samsung1.png",
                    MRPAmount = 15999,
                    DiscountPercentage = 10,
                    InStock = true,
                    MaxOrderAmount = 3,
                    InventoryId = "Mob-Sam-1"
                },
                new Product
                {
                    Id=4,
                    Name = "Iphone 13 Max pro",
                    Image = "IPhone1.png",
                    MRPAmount = 15999,
                    DiscountPercentage = 10,
                    InStock = true,
                    MaxOrderAmount = 3,
                    InventoryId = "Mob-iphone-1"
                });
           
        }
    }
}
