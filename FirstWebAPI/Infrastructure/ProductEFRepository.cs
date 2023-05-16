using FirstWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstWebAPI.Infrastructure
{
    public class ProductEFRepository : IAsyncRepository<Product, int>
    {
        NorthwindContext _context;
        public ProductEFRepository(NorthwindContext context) => _context = context;

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await  _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ProductId == id)!;
        }

        public async Task RemoveAsync(int id)
        {
            var item = await _context.Products.AsNoTracking()
                .FirstOrDefaultAsync(c => c.ProductId == id);
            if (item is not null)
            {
                _context.Products.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpsertAsync(Product input)
        {
            var item = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ProductId == input.ProductId);
            if (item is not null)
            {
                _context.Products.Update(input);
                await _context.SaveChangesAsync();
            } else
            {
                _context.Products.Add(input);
                await _context.SaveChangesAsync();
            }
        }
    }
}
