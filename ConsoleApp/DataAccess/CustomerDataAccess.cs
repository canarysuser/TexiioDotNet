using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.DataAccess
{
    public class CustomerDataAccess : BaseDataAccess
    {
        public List<Customer> GetAllCustomers()
        {
            string sql = "SELECT CustomerId, CompanyName, ContactName, City, Country FROM Customers "; 
            List<Customer> list = new List<Customer>();
            try
            {
                OpenConnection();
                var reader = ExecuteQuery(sql, System.Data.CommandType.Text); 
                while (reader.Read())
                {
                    list.Add(new Customer
                    {
                        CustomerId = reader.GetString(0),
                        CompanyName = reader.GetString(1),
                        ContactName = reader.GetString(2),
                        City = reader.GetString(3),
                        Country = reader.GetString(4)
                    });
                }
                reader.Close();
            } 
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { CloseConnection(); }
            return list;
        }

        public Customer GetCustomerDetails(string customerId)
        {
            string sql = "SELECT CustomerId, CompanyName, ContactName, City, Country " +
                //" FROM Customers WHERE CustomerId = '" + customerId + "'";
                " FROM Customers WHERE CustomerId = @customerId ";
            Customer customer = null!;
            SqlParameter idParm = new SqlParameter();
            idParm.Value = customerId;
            idParm.ParameterName = "@customerId";
            idParm.SqlDbType = System.Data.SqlDbType.VarChar;
            idParm.Size = 5;
            try
            {
                OpenConnection();
                //var reader = ExecuteQuery(sql, System.Data.CommandType.Text);
                var reader = ExecuteQuery(sql, System.Data.CommandType.Text, idParm);
                while (reader.Read())
                {
                    customer = new Customer
                    {
                        CustomerId = reader.GetString(0),
                        CompanyName = reader.GetString(1),
                        ContactName = reader.GetString(2),
                        City = reader.GetString(3),
                        Country = reader.GetString(4)
                    };
                }
                reader.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { CloseConnection(); }
            return customer;
        }

        public void InsertNewCustomer(Customer customer)
        {
            string sql = "INSERT INTO Customers(CustomerId, CompanyName, ContactName, City, Country) " +
                //" FROM Customers WHERE CustomerId = '" + customerId + "'";
                " VALUES (@customerId, @company, @contact, @city, @country) ";
            SqlParameter p1 = new SqlParameter("@customerId", customer.CustomerId);
            SqlParameter p2 = new SqlParameter("@company", customer.CompanyName);
            SqlParameter p3 = new SqlParameter("@contact", customer.ContactName);
            SqlParameter p4 = new SqlParameter("@city", customer.City);
            SqlParameter p5 = new SqlParameter("@country", customer.Country);
            try
            {
                OpenConnection();
                var command = connection.CreateCommand(); //basedataAccess.connection
                command.CommandText = sql;
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.Add(p1);
                command.Parameters.Add(p2);
                command.Parameters.Add(p3);
                command.Parameters.Add(p4);
                command.Parameters.Add(p5);
                command.ExecuteNonQuery();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { CloseConnection(); }
            return;
        }
        public void UpdateCustomer(Customer customer)
        {
            //string sql = "UPDATE Customers SET CompanyName=@company, ContactName=@contact, " +
            //    " City= @city, Country =  @country WHERE CustomerId=@customerId ";
            string sql = "sp_UpdateCustomer";
            SqlParameter p1 = new SqlParameter("@customerId", customer.CustomerId);
            SqlParameter p2 = new SqlParameter("@company", customer.CompanyName);
            SqlParameter p3 = new SqlParameter("@contact", customer.ContactName);
            SqlParameter p4 = new SqlParameter("@city", customer.City);
            SqlParameter p5 = new SqlParameter("@country", customer.Country);
            try
            {
                OpenConnection();
                var command = connection.CreateCommand(); //basedataAccess.connection
                command.CommandText = sql;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(p1);
                command.Parameters.Add(p2);
                command.Parameters.Add(p3);
                command.Parameters.Add(p4);
                command.Parameters.Add(p5);
                command.ExecuteNonQuery();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { CloseConnection(); }
            return;
        }
    }
}
