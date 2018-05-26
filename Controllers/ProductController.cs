
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Common;
using ProductApi.Data;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [Route("api/")]
    public class ProductController : Controller
    {
        private readonly ProductDbContext productDbContext;

        public ProductController(ProductDbContext context)
        {
            this.productDbContext = context;
        }

       [HttpGet]
       [Route("products")]
       public IActionResult Get()
       {
          return Ok(this.productDbContext.Products.ToList());
       }

       [HttpGet]
       [Route("product/{id}")]
       public IActionResult Get(string id)
       {
           var result = this.productDbContext.Products
            .FirstOrDefault(x => x.ProductId.ToString() == id);

            if (result == null)
                return new NotFoundResult();

            return Ok(result);
       }

       [HttpPost]
       [RequireUser(Order = 0)]
       [RequireUserRole(Order = 1, AllowedRoles = new string[] { "admin" })]
       [Route("product")]
       public IActionResult Create([FromBody]Product product)
       {
           if (!ModelState.IsValid)
           {
               return BadRequest(ModelState);
           }

           this.productDbContext.Products.Add(product);
           this.productDbContext.SaveChanges();

           return Created(product.ProductId.ToString(), product);
       }

       [HttpPut]
       [RequireUser(Order = 0)]
       [RequireUserRole(Order = 1, AllowedRoles = new string[] { "admin" })]
       [Route("product/{id}")]
       public IActionResult Update(string id, [FromBody]Product product)
       {
           if (!ModelState.IsValid)
           {
               return BadRequest(ModelState);
           }

           var result = this.productDbContext.Products.FirstOrDefault(x => x.ProductId.ToString() == id);
           if (result == null)
           {
               return NotFound();
           }

           result.Count = product.Count;
           result.Description = product.Description;
           result.Name = product.Name;
           result.Price = product.Price;
           this.productDbContext.SaveChanges();

           return NoContent();
       }

       [HttpDelete]
       [RequireUser(Order = 0)]
       [RequireUserRole(Order = 1, AllowedRoles = new string[] { "admin" })]
       [Route("product/{id}")]
       public IActionResult Delete(string id)
       {
           var result = this.productDbContext.Products.FirstOrDefault(x => x.ProductId.ToString() == id);
           if (result == null)
           {
               return NotFound();
           }

           this.productDbContext.Remove(result);
           this.productDbContext.SaveChanges();
           return NoContent();
       }
    }
}