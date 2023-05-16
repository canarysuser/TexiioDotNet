using FirstWebApp.Models;
using Microsoft.EntityFrameworkCore;


//NUGET: Microsoft.EntityFrameworkCore.SqlServer

namespace FirstWebApp.Infrastructure
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }


    }
}
