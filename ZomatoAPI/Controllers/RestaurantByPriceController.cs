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
    public class RestaurantByPriceController : ApiController
    {
        [HttpGet]
        [Route("api/restaurant/price")]
        public async Task<IHttpActionResult> RestaurantByPrice([FromUri] string order)
        {
            using (var db = new ApplicationDbContext())
            {
                var restaurants = await db.Restaurants
                    .Include(r => r.Dishes) .ToListAsync();      

                // Apply sorting logic in memory
                var result = restaurants.Select(r => new
                {
                    r.Name,
                    r.City,
                    r.Rating,
                    Dishes = order.ToLower() == "asc"
                        ? r.Dishes.OrderBy(d => d.Price).Select(d => new
                        {
                            d.Name,
                            d.Price,
                            d.VegOrNonVeg
                        }).ToList()
                        : r.Dishes.OrderByDescending(d => d.Price).Select(d => new
                        {
                            d.Name,
                            d.Price,
                            d.VegOrNonVeg
                        }).ToList()
                });

                return Ok(result);
            }
        }

        [HttpGet]
        [Route("api/restaurant/dishes")]
        public async Task<IHttpActionResult> SearchDishes([FromQuery] string dishName)
        {
            using (var db = new ApplicationDbContext())
            {
                var restaurantsWithDishes = await db.Restaurants
                    .Include(r => r.Dishes)
                    .Where(r => r.Dishes.Any(d => d.Name.Contains(dishName)))
                    .Select(r => new
                    {
                        r.Name,
                        r.City,
                        r.Rating,
                        Dishes = r.Dishes
                            .Where(d => d.Name.Contains(dishName))
                            .Select(d => new
                            {
                                d.Name,
                                d.Price,
                                d.VegOrNonVeg
                            }).ToList()
                    })
                    .ToListAsync();

                return Ok(restaurantsWithDishes);
            }
        }

    }
}
