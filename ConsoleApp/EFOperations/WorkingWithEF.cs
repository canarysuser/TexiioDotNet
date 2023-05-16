using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.EFOperations
{
    public class WorkingWithEF
    {
        internal static void Test()
        {
            CustomerRepository customerRepository = new CustomerRepository();
            var list = customerRepository.GetAll();
            Console.WriteLine("*** LIst of Customers *** ");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            string id = "ALFKI";
            var cust = customerRepository.Get(id);
            Console.WriteLine("*** Details for 'ALFKI' ******");
            Console.WriteLine(cust);
            cust.CustomerId = "23458"; 
            customerRepository.Create(cust);
            cust = customerRepository.Get(cust.CustomerId);
            Console.WriteLine($"AFter inserting: \n{cust}");
            Customer customer = new Customer
            {
                CustomerId = cust.CustomerId,
                CompanyName = "UPDATED using EF",
                ContactName = cust.ContactName,
                City = cust.City,
                Country = "EFCountry"
            };
            customerRepository.Update(customer);
            cust = customerRepository.Get(customer.CustomerId);
            Console.WriteLine($"AFter updating: \n{cust}");
            customerRepository.Delete(cust.CustomerId);
            list = customerRepository.GetAll();
            Console.WriteLine("After DELETING - LIst of Customers *** ");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}
