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
    public class AllItemsController : ApiController
    {
        [HttpGet]
        [Route("api/allitem")]
        public async Task<IHttpActionResult> GetAllItems()
        {
            using (var db = new ApplicationDbContext())
            {
                var restaurants = await db.Restaurants
                    .Select(r => new
                    {
                        r.Name,
                        r.City,
                        r.Rating,
                        Dishes = r.Dishes.Select(d => new
                        {
                            d.Name,
                            d.VegOrNonVeg,
                            d.Price,
                            Category = d.Category.Name,
                        })
                    }).ToListAsync();

                return Ok(new { Restaurants = restaurants});
            }
        }
    }
}
