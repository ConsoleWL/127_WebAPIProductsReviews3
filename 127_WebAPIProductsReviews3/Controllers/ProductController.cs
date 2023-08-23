using _127_WebAPIProductsReviews3.Data;
using _127_WebAPIProductsReviews3.Models;
using _127_WebAPIProductsReviews3.Models.Dto;
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
            double price = 0;

            try
            {
                price = Convert.ToDouble(maxPrice);

            }
            catch (Exception)
            {
                return BadRequest("The maxPrice shoud contain only numbers!");
            }

            List<ProductDTO> products = _context.Products
                .Select(d => new ProductDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                    Price = d.Price,
                    AverageRating = d.Reviews.Average(r => r.Rating),
                    Reviews = d.Reviews.Select(c => new ReviewDTO
                    {
                        Id = c.Id,
                        Text = c.Text,
                        Rating = c.Rating
                    }).ToList()
                })
                .ToList();

            if (maxPrice != null)
            {
                products = products.Where(f => f.Price <= price).ToList();
            }
            return Ok(products);

        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            var product = _context.Products
                .Where(f => f.Id == id)
                .Select(d => new ProductDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                    Price = d.Price,
                    Reviews = d.Reviews.Select(r => new ReviewDTO
                    {
                        Id = r.Id,
                        Text = r.Text,
                        Rating = r.Rating
                    }).ToList(),
                    AverageRating =  d.Reviews.Average(r=>r.Rating) // how to round up in here? to show 2 numbers after decimal. I tried. Math.Round(d.Reviews.Average(r=>r.Rating),2) but it doesn't seem to be working
                });

            if (product is null)
                return Ok(product);

            return Ok(product);

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
