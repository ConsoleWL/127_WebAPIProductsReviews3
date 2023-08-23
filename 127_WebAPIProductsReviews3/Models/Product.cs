using System.ComponentModel.DataAnnotations;

namespace _127_WebAPIProductsReviews3.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        // nav prop
        public ICollection<Review> Reviews { get; set; }
    }
}
