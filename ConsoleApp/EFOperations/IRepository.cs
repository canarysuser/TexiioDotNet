using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.EFOperations
{
    public interface IRepository<TEntity, TIdentity> 
        //where TEntity - represents the entity class and TIDentity is the type of the ID Column
    {
        List<TEntity> GetAll();
        TEntity Get(TIdentity id);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TIdentity id);
    }
    public class CustomerRepository : IRepository<Customer, string>
    {
        NorthwindDataContext _db = new NorthwindDataContext(); 
        
        public List<Customer> GetAll()
        {
            return _db.Customers
                .AsNoTracking()
                .ToList();
        }

        public Customer Get(string id)
        {
            return _db.Customers
                .AsNoTracking()
                .FirstOrDefault(c => c.CustomerId.Equals(id));
        }
        public void Create(Customer entity)
        {
            var customer = _db.Customers.AsNoTracking().FirstOrDefault(c => c.CustomerId == entity.CustomerId);
            if(customer is null)
            {
                _db = new NorthwindDataContext();
                _db.Customers.Add(entity);
                _db.SaveChanges();
            }
        }
        public void Delete(string id)
        {
            var customer = _db.Customers.AsNoTracking().FirstOrDefault(c => c.CustomerId == id);
            if (customer is not null)
            {
                _db = new NorthwindDataContext();
                _db.Customers.Remove(customer);
                _db.SaveChanges();
            }
        }
        public void Update(Customer entity)
        {
            var customer = _db.Customers.AsNoTracking().FirstOrDefault(c => c.CustomerId == entity.CustomerId);
            if (customer is not null)
            {

                _db = new NorthwindDataContext();
                _db.Customers.Update(entity) ;
                _db.SaveChanges();
            }
        }
    }
}
