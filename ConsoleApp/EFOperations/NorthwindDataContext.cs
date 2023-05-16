using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.EFOperations
{
    public class NorthwindDataContext : DbContext
    {
        public NorthwindDataContext() :base()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            //sqlConnectionStringBuilder.TrustServerCertificate
            optionsBuilder
                .UseSqlServer(
                    connectionString: @"Server=SNWIN10WK;Database=Northwind;Integrated Security=SSPI;TrustServerCertificate=true")
                .EnableSensitiveDataLogging(true)
                .LogTo(Console.Write);

        }
        public DbSet<Customer> Customers { get; set; }
    }
}
