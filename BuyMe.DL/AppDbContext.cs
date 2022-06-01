﻿using BuyMe.DL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuyMe.DL
{
    public class AppDbContext :IdentityDbContext<BuyMeUser> // this inherits from DbContext
    {
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder builder)      
        {
            base.OnModelCreating(builder);
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
                    InventoryId="Mob-oppo-1",
                    CategoryId = 4
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
                    InventoryId = "Mob-vivo-1",
                    CategoryId = 4
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
                    InventoryId = "Mob-Sam-1",
                    CategoryId=4
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
                    InventoryId = "Mob-iphone-1",
                    CategoryId = 4
                });

            builder.Entity<Category>().HasData(
                new Category
                {
                    Id=1,
                    Name="Books",
                    Description="Test",
                    
                },
                new Category
                {
                    Id = 2,
                    Name = "Games",
                    Description = "Test"
                },
                new Category
                {
                    Id = 3,
                    Name = "Tools",
                    Description = "Test"
                },
                new Category
                {
                    Id = 4,
                    Name = "Mobiles",
                    Description = "Test"
                },
                new Category
                {
                    Id = 5,
                    Name = "Laptops",
                    Description = "Test"
                }




            );
           
        }
    }
}
