using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZomatoAPI.Models;

namespace ZomatoAPI.Controllers
{
    public class DishController : ApiController
    {
        [HttpGet]
        [Route("api/dishes")]
        public IHttpActionResult GetAllDishes()
        {
            using(var db = new ApplicationDbContext())
            {
                var dishes = db.Dishes.Select(x=>new
                {
                    x.DishId,
                    x.Name,
                    x.VegOrNonVeg,
                    x.Price,
                    x.CategoryId,
                    x.RestaurantId,
                }).ToList();

                return Ok(dishes);
            }
        }

        [HttpGet]
        [Route("api/dishes/id")]
        public IHttpActionResult GetDishById([FromQuery] int disid)
        {
            using (var db = new ApplicationDbContext())
            {
                var ans = db.Dishes.Where(x=>x.DishId == disid).ToList();
                return Ok(ans);
            }
        }

        [HttpPost]
        [Route("api/dishes")]
        public IHttpActionResult AddDishes(Dish dish)
        {
            if(dish == null)
            {
                return BadRequest("Invalid Dish data.");
            }

            using(var db = new ApplicationDbContext())
            {
                var restaurantexist = db.Restaurants.Any(x=>x.RestaurantId == dish.RestaurantId);
                var categoryExistes = db.Categories.Any(u=>u.CategoryId == dish.CategoryId);

                if(!restaurantexist || !categoryExistes)
                {
                    return BadRequest("Invalid restaurant or category!");
                }

                db.Dishes.Add(dish);
                db.SaveChanges();
                return Ok("Dish Added Successfully");
            }
        }
    }
}
