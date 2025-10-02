using Ecommerce.StockService.Data;
using Ecommerce.StockService.Interfaces;
using Ecommerce.StockService.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.StockService.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StockDbContext _context;

        public ProductRepository(StockDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Product order)
        {
            await _context.Products.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                 .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
