﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZomatoAPI.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public ICollection<Dish> Dishes {  get; set; }  
    }
}