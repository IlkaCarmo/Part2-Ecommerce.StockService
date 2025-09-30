using Ecommerce.StockService.Models;

namespace Ecommerce.StockService.Interfaces
{
    public interface IProductRepository
    {
        Task CreateAsync(Product order);
        Task<Product?> GetByIdAsync(Guid id);
    }
}
