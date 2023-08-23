using System.ComponentModel.DataAnnotations.Schema;

namespace _127_WebAPIProductsReviews3.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }

        // nav props
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
