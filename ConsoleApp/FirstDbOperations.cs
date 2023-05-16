using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ConsoleApp.DataAccess;

namespace ConsoleApp
{
    internal class FirstDbOperations
    {
        internal static void Test()
        {
            string connectionString = @"Server=SNWIN10WK;Database=Northwind;Integrated Security=SSPI"; 
            SqlConnection connection = new SqlConnection(); 
            connection.ConnectionString = connectionString; 
            connection.Open();
            SqlCommand command = new SqlCommand(); 
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT CustomerId, CompanyName, ContactName, City, Country FROM Customers";
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader(); 
            while(reader.Read())
            {
                Console.WriteLine("{0,-5}, {1,-45}, {2,-20}, {3,-20}, {4,-20} ", 
                    reader.GetString(0),
                    reader[1],
                    reader["ContactName"],
                    reader.GetString(3),
                    reader.GetString(4)
                );
            }
            reader.Close(); 
            connection.Close();
        }

        internal static void Test2()
        {
            CustomerDataAccess dataAccess = new CustomerDataAccess();
            var list = dataAccess.GetAllCustomers();
            foreach( var item in list )
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("".PadLeft(45, '='));
            Console.Write("Enter CustomerID to fetch: "); 
            string custId = Console.ReadLine();
            var obj = dataAccess.GetCustomerDetails(custId);
            if(obj == null )
                Console.WriteLine("No records found.");
            else
                Console.WriteLine(obj);
            Console.WriteLine("".PadLeft(45, '='));
            //TO Insert into Customers
            Customer cust = new Customer
            {
                CustomerId = "98766",
                CompanyName = "ABCDEF",
                ContactName = "ABCDEF",
                City = "ABCDE",
                Country = "ABCDE"
            };
            //dataAccess.InsertNewCustomer(cust);
            Console.WriteLine("".PadLeft(45, '='));
            list = dataAccess.GetAllCustomers();
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("".PadLeft(45, '='));
            //UPDATE Operations 
            Console.WriteLine("Press a key to update customer row.");
            Console.ReadKey();
            Console.Clear();
            Console.Write("Enter the Customer Id: ");
            string id = Console.ReadLine();
            //Check whether this ID exists, if yes, display the current data and accept the new data. 
            obj = dataAccess.GetCustomerDetails(id);
            if(obj is null)
            {
                Console.WriteLine("No matching records found.");
                Console.WriteLine("Press a key to terminate.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine($"Details for customer {id}. ");
            Console.WriteLine("Previous values displayed in brackets. Press enter to accept previous values.");
            Console.Write($"Company Name [{obj.CompanyName}] :");
            string company = Console.ReadLine();
            if (string.IsNullOrEmpty(company.Trim())) company = obj.CompanyName;
            Console.Write($"Contact Name [{obj.ContactName}] :");
            string contact = Console.ReadLine();
            if (string.IsNullOrEmpty(contact.Trim())) contact = obj.ContactName;
            Console.Write($"City [{obj.City}] :");
            string city = Console.ReadLine();
            if (string.IsNullOrEmpty(city.Trim())) city = obj.City;
            Console.Write($"Country [{obj.Country}] :");
            string country = Console.ReadLine();
            if (string.IsNullOrEmpty(country.Trim())) country = obj.Country;
            Console.WriteLine("\nIs this OKAY? [Y/N]: ");
            string choice= Console.ReadLine();
            if (choice.Trim().ToUpper().Contains("Y"))
            {
                var newObj = new Customer { CustomerId = obj.CustomerId ,
                CompanyName=company, ContactName=contact, City=city, Country=country};
                dataAccess.UpdateCustomer(newObj);
            }
        }
    }
}
