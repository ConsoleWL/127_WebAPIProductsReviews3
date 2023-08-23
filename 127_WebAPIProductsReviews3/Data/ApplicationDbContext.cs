using Microsoft.EntityFrameworkCore;

namespace _127_WebAPIProductsReviews3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
