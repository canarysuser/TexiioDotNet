using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.EFOperations
{
    public class Customer
    {
        [Key]
        public string CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public override string ToString()
        {
            return $"{CustomerId,-5}, {CompanyName,-45}, {ContactName,-20}, {City,-20}, {Country,-20} ";
        }
    }
}
