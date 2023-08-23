using _127_WebAPIProductsReviews3.Data;
using _127_WebAPIProductsReviews3.Models;
using Microsoft.AspNetCore.Mvc;

namespace _127_WebAPIProductsReviews3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Review> reviews = _context.Reviews.ToList();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Review review = _context.Reviews.FirstOrDefault(f => f.Id == id);
            if (review is null)
                return NotFound();

            return Ok(review);
        }

        [HttpGet("getbyproductid/{id}")]
        public IActionResult GetByPRoductId(int id)
        {
            List<ICollection<Review>> reviews = _context.Products
                .Where(f => f.Id == id)
                .Select(f => f.Reviews)
                .ToList();
            
            return Ok(reviews);
        }

        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody] Review review)
        {
            if (review is null)
                return NotFound();
            
            review.ProductId = id;

            _context.Reviews.Add(review);
            _context.SaveChanges();
            return Ok(review);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Review review)
        {
            if (review is null)
                return BadRequest();

            Review eReview = _context.Reviews.FirstOrDefault(f => f.Id == id);
            if (eReview is null)
                return NotFound();

            eReview.Text = review.Text;
            eReview.Rating = review.Rating;

            _context.SaveChanges();
            return Ok(review);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Review review = _context.Reviews.FirstOrDefault(f => f.Id == id);
            if (review is null)
                return NotFound();

            _context.Reviews.Remove(review);
            _context.SaveChanges();
            return Ok("Deleted");

        }


    }
}
