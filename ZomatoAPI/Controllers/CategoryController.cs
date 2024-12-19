using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZomatoAPI.Models;

namespace ZomatoAPI.Controllers
{
    public class CategoryController : ApiController
    {
        [HttpGet]
        [Route("api/categories")]
        public IHttpActionResult GetAllCategories()
        {
            using(var db = new ApplicationDbContext())
            {
                var categories = db.Categories.Select(u=> new
                {
                    u.CategoryId,
                    u.Name,
                }).ToList();

                return Ok(categories);
            }
        }

        [HttpPost]
        [Route("api/categories")]
        public IHttpActionResult AddCategory(Category category)
        {
            if(category == null)
            {
                return BadRequest("Inavlid categroy data!");
            }

            using(var db = new ApplicationDbContext())
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return Ok("Category Added Successfully!");
            }
        }
    }
}
