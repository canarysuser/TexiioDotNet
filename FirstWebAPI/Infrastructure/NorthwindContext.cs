using FirstWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstWebAPI.Infrastructure
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }    
    }
}
