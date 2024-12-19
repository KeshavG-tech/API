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
    public class SortController : ApiController
    {
        [HttpGet]
        [Route("api/restaurant/sort")]
        public async Task<IHttpActionResult> Sorting([FromQuery] string filter)
        {
            if(string.IsNullOrEmpty(filter))
            {
                return BadRequest("Sort filter cannot be empty");
            }

            using(var db = new ApplicationDbContext())
            {
                IQueryable<Restaurant> query = db.Restaurants;

                switch(filter.ToLower())
                {
                    case "city":
                        query = query.OrderBy(x => x.City);
                        break;
                    case "rating":
                        query = query.OrderByDescending(x => x.Rating);
                        break;
                    default:
                        return BadRequest("Invalid filter specified");
                }

                var result = await query.Select(x => new
                {
                    x.Name,
                    x.City,
                    x.Rating,
                    Dishes = x.Dishes.Select(u=> new
                    {
                        u.Name,
                        u.VegOrNonVeg,
                        u.Price,
                        u.CategoryId,
                    })
                }).ToListAsync();
                return Ok(result);
            }
        }
    }
}
