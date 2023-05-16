using FirstWebApp.Models;

namespace FirstWebApp.Infrastructure
{
    public class CustomerListRepository : IRepository<Customer, string>
    {
        public static List<Customer> customerList = new List<Customer>
        {
            new Customer{CustomerId="12345", CompanyName="12345", ContactName="12345", City="12345", Country="12345"},
            new Customer{CustomerId="23456", CompanyName="23456", ContactName="23456", City="23456", Country="23456"},
            new Customer{CustomerId="34567", CompanyName="34567", ContactName="34567", City="34567", Country="34567"},
            new Customer{CustomerId="45678", CompanyName="45678", ContactName="45678", City="45678", Country="45678"}
        };
        public Customer Get(string id)
        {
            return customerList.FirstOrDefault(c => c.CustomerId == id)!;
        }

        public List<Customer> GetAll()
        {
            return customerList;
        }
        public void Create(Customer entity)
        {
            var item = customerList.Find(c=>c.CustomerId == entity.CustomerId);
            if(item is null)
                customerList.Add(entity);
        }

        public void Delete(string id)
        {
            var item = customerList.Find(c => c.CustomerId == id);
            if (item is not null)
            {
                customerList.Remove(item);
            }
        }

        public void Update(Customer entity)
        {
            var item = customerList.Find(c => c.CustomerId == entity.CustomerId);
            if (item is not null)
            {
                customerList.Remove(item);
                customerList.Add(entity);
            }
        }
    }
}
