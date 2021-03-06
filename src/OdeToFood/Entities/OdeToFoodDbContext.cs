﻿using Microsoft.EntityFrameworkCore;

namespace OdeToFood.Entities
{
    public class OdeToFoodDbContext : DbContext
    {
        public OdeToFoodDbContext(DbContextOptions options) : base(options)
        {

        }

        public OdeToFoodDbContext()
        {

        }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
