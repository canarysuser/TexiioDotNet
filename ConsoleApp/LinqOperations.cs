using ConsoleApp.EFOperations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class LinqOperations
    {
        static NorthwindDataContext db = new NorthwindDataContext(); 
        internal static void Test()
        {
            //BasicQuerying();
            //ProjectionOperator();
            //RestrictionOperator();
            //PartitionOperators();
            //SortingOperators();
            //AggregateOperators();
            //ElementOperators();
            ComplexQuery();
        }
        static void ComplexQuery()
        {
            Console.Write("Enter text to search: "); 
            string criteria = Console.ReadLine();
            var parameters = criteria.Split(' ');
            var query = db.Customers
                .AsNoTracking()
                .ToList()
                .Where(c => parameters.Any(p =>
                        c.CompanyName.Contains(p, StringComparison.OrdinalIgnoreCase) ||
                        c.ContactName.Contains(p, StringComparison.OrdinalIgnoreCase) ||
                        c.City.Contains(p, StringComparison.OrdinalIgnoreCase) ||
                        c.Country.Contains(p, StringComparison.OrdinalIgnoreCase)
                    ));

            query.ToList().ForEach(c=>Console.WriteLine(c));

        }
        static void ElementOperators()
        {
            //First, Last, Single, ElementAt FirstOrDefault, .... 
            var first = db.Customers.First();
            var last = db.Customers.OrderBy(c=>c.Country).Last();
            var middle = db.Customers.ToList().ElementAt(50);
            Console.WriteLine($"First:\n{first}\nLast:\n{last}\nElement At 50:\n{middle}");
            //first = db.Customers.First(c => c.Country.Equals("Japan"));
            first = db.Customers.FirstOrDefault(c => c.Country.Equals("Japan"));
            Console.WriteLine("First Japanese:\n{0}", first);
        }
        static void AggregateOperators()
        {
            var numbers = Enumerable.Range(start: 101, count: 150);
            Console.WriteLine($"Count: {numbers.Count()}");
            Console.WriteLine($"Sum: {numbers.Sum()}");
            Console.WriteLine($"Average: {numbers.Average()}");
            Console.WriteLine($"Min: {numbers.Min()}");
            Console.WriteLine($"Max: {numbers.Max()}");
        }
        static void SortingOperators()
        {
            //OrderBy, OrderByDescending, ThenBy, ThenByDescending 
            //query syntax -> orderby propertyName ascending|descending, secondPropertyName
            //var q1 = from c in db.Customers
            //         where c.CompanyName.Contains("de")
            //         orderby c.CompanyName descending
            //         select c;
            //foreach (var c in q1) { Console.WriteLine(c); }
            var q2 = db.Customers
                     .Where(c=> c.CompanyName.Contains("de"))
                     //.OrderBy(c=>c.CompanyName)
                     .OrderBy(c=>c.Country)
                     .ThenByDescending(c=>c.CompanyName)
                     .Select(c=>c);
            foreach (var c in q2) { Console.WriteLine(c); }
            var q1 = from c in db.Customers
                     where c.CompanyName.Contains("de")
                     orderby c.Country, c.CompanyName descending
                     select c;
            foreach (var c in q1) { Console.WriteLine(c); }
        }
        static void PartitionOperators()
        {
            //Divide the result set into a smaller subset.
            //Take, Skip -- supported by the Method Syntax, cannot use the query syntax 
            //var q1 = (from c in db.Customers select c).Take(10);
            //foreach (var c in q1) { Console.WriteLine(c); }
            //Console.WriteLine();
            //var q2 = (from c in db.Customers select c).Skip(50);
            //foreach (var c in q2) { Console.WriteLine(c); }
            //Console.WriteLine();
            var q3 = (from c in db.Customers select c)
                .Skip(25)
                .Take(15)
                .Skip(10)
                .Take(5);
            foreach (var c in q3) { Console.WriteLine(c); }

        }
        static void RestrictionOperator()
        {
            //WHERE clause 
            //var q1 = from c in db.Customers
            //         where c.Country.Contains("USA")
            //         select c;
            //foreach (var c in q1) { Console.WriteLine(c); }
            //var q2 = db.Customers
            //         .Where(c => c.Country.Contains("USA"))
            //         .Select(c => c);
            //foreach (var c in q2) { Console.WriteLine(c); }

            var q1 = from c in db.Customers
                     where c.CompanyName.StartsWith("A") && c.Country.Contains("U")
                     select c;
            foreach (var c in q1) { Console.WriteLine(c); }
        }
        static void ProjectionOperator()
        {
            //Select 
            var q1 = from c in db.Customers
                     select new { 
                         CustomerId = c.CustomerId,
                         CompanyNameDetails = c.CompanyName + "-" + c.City +"-" + c.Country
                     };
            foreach(var c in q1)
            {
                Console.WriteLine($"Id: {c.CustomerId}, Details: {c.CompanyNameDetails}");
            }
        }
        static void BasicQuerying()
        {
            //QUERY Syntax
            var q1 = from c in db.Customers select c; 
            foreach(var c in q1) { Console.WriteLine(c); }
            //Method Syntax 
            var q2 = db.Customers.Select(c=>c);
            foreach (var c in q2) { Console.WriteLine(c); }
        }
    }
}
