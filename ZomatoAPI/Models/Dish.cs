using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZomatoAPI.Models
{
    public class Dish
    {
        public int DishId { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        public bool VegOrNonVeg { get; set; }

        public decimal Price { get; set; }
    }
}