﻿using Microsoft.EntityFrameworkCore;
using PieShop.Models;

namespace PieShop.Data
{
    public class PieShopDbContext : DbContext
    {
        public PieShopDbContext(DbContextOptions<PieShopDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Pie> Pies { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
