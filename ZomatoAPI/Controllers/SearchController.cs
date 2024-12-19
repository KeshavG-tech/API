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
    public class SearchController : ApiController
    {
        [HttpGet]
        [Route("api/search")]
        public async Task<IHttpActionResult> SearchByName([FromQuery] string query)
        {
            if(string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Search query cannot be empty.");
            }

            using(var db = new ApplicationDbContext())
            {
                var restaurants = await db.Restaurants.Where(x => x.Name.Contains(query) || x.City.Contains(query)).Select(u => new
                {
                    u.Name,
                    u.City,
                    u.Rating,
                }).ToListAsync();


                var dishes = await db.Dishes.Where(x => x.Name.Contains(query)).Select(u => new
                {
                    u.Name,
                    u.VegOrNonVeg,
                    u.Price,
                    u.CategoryId,
                    u.RestaurantId,
                }).ToListAsync();

                return Ok(new {Restaurant = restaurants , Dishes = dishes});
            }
        }
    }

    internal class FromQueryAttribute : Attribute
    {
    }
}
