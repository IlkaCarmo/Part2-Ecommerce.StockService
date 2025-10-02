using Ecommerce.StockService.DTOs;
using Ecommerce.StockService.Interfaces;
using Ecommerce.StockService.Models;

namespace Ecommerce.StockService.Services
{
    public class ProductServer 
    {
        private readonly IProductRepository _productRepository;
        private readonly IRabbitMqPublisher _rabbitMqPublisher;

        public ProductServer(IProductRepository productRepository, IRabbitMqPublisher rabbitMqPublisher)
        {
            _productRepository = productRepository;
            _rabbitMqPublisher = rabbitMqPublisher;
        }

        public async Task<ProductResponseDto>  CreateAsync(ProductRequest request)
        {
            var product = new Product
            {
                Id = request.Id,
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            };

            await _productRepository.CreateAsync(product);

            var response = new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };

            return response;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }
    }
}
