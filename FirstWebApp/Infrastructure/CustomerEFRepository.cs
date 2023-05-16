using FirstWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApp.Infrastructure
{
    public class CustomerEFRepository : IRepository<Customer, string>
    {
        NorthwindContext _context;
        public CustomerEFRepository(NorthwindContext context) => _context = context;

        public void Create(Customer entity)
        {
            var item = _context.Customers.AsNoTracking().FirstOrDefault(c=>c.CustomerId==entity.CustomerId);
            if(item is null) { 
                _context.Customers.Add(entity);
                _context.SaveChanges();
            }
        }
        public void Delete(string id)
        {
            var item = _context.Customers.AsNoTracking().FirstOrDefault(c => c.CustomerId == id);
            if (item is not null)
            {
                _context.Customers.Remove(item);
                _context.SaveChanges();
            }
        }
        public void Update(Customer entity)
        {
            var item = _context.Customers.AsNoTracking().FirstOrDefault(c => c.CustomerId == entity.CustomerId);
            if (item is not null)
            {
                _context.Customers.Update(entity);
                _context.SaveChanges();
            }
        }
        public Customer Get(string id)
        {
            return _context.Customers.AsNoTracking().FirstOrDefault(c => c.CustomerId == id)!;
        }

        public List<Customer> GetAll()
        {
            return _context.Customers.AsNoTracking().ToList();
        }

       
    }
}
