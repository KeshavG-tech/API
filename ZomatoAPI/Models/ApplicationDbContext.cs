using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ZomatoAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=DefaultConn")
        {

        }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Dish> Dishes { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}