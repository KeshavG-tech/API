using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ZomatoAPI.Models;

namespace ZomatoAPI.Controllers
{
    public class RestaurantController : ApiController
    {
        [HttpGet]
        [Route("api/restaurants")]
        public IHttpActionResult GetAllRestaurants()
        {
            using(var db = new ApplicationDbContext())
            {
                var restaurants = db.Restaurants.Select(x=> new
                {
                    x.RestaurantId,
                    x.Name,
                    x.City,
                    x.Rating,
                }).ToList();
                return Ok(restaurants);
            }
        }

        [HttpGet]
        [Route("api/restaurants/id")]
        public IHttpActionResult GetRestaurantById([FromQuery] int restid)
        {
            using(var db = new ApplicationDbContext())
            {
                var ans = db.Restaurants.Where(x=>x.RestaurantId == restid).ToList();
                return Ok(ans);
            }
        }

        [HttpPost]
        [Route("api/restaurants")]
        public IHttpActionResult AddRestaurant(Restaurant restaurant)
        {
            if(restaurant == null)
            {
                return BadRequest("Invalid restaurnt data!");
            }
            using(var db = new ApplicationDbContext())
            {
                db.Restaurants.Add(restaurant);
                db.SaveChanges();
                return Ok("Restaurant Added Successfully!!");
            }
        }
    }
}
