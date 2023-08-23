﻿using _127_WebAPIProductsReviews3.Data;
using _127_WebAPIProductsReviews3.Models;
using Microsoft.AspNetCore.Mvc;

namespace _127_WebAPIProductsReviews3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string? maxPrice)
        {
            double price;
            try
            {
                price = Convert.ToDouble(maxPrice);

            }
            catch (Exception)
            {
                return BadRequest("maxPrice must be a number");
                throw;
            }

            List<Product> products = _context.Products.ToList();

            if (maxPrice != null)
                products = products.Where(f => f.Price <= price).ToList();

            return Ok(products);

        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (product is null)
                return BadRequest();

            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            if (product is null)
                return BadRequest();

            Product eProduct = _context.Products.FirstOrDefault(f => f.Id == id);
            if (eProduct is null)
                return NotFound();

            eProduct.Name = product.Name;
            eProduct.Price = product.Price;

            _context.SaveChanges();
            return Ok(eProduct);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product product = _context.Products.FirstOrDefault(f => f.Id == id);
            if (product is null)
                return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok("Deleted");

        }
    }
}