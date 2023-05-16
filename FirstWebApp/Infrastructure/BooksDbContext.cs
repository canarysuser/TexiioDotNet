using FirstWebApp.Models;
using Microsoft.EntityFrameworkCore;


//NUGET: Microsoft.EntityFrameworkCore.SqlServer

namespace FirstWebApp.Infrastructure
{
    public class BooksDbContext : DbContext
    {
        public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasKey(c => c.BookId);
        }


    }
}
