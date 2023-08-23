namespace _127_WebAPIProductsReviews3.Models.Dto
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        // nav prop
        public ICollection<ReviewDTO> Reviews { get; set; }

        public double AverageRating { get; set; }
    }
}
