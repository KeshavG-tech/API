using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZomatoAPI.Models
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public double Rating { get; set; }

        public ICollection<Dish> Dishes { get; set; }
    }
}