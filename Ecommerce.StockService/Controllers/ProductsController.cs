using Ecommerce.StockService.DTOs;
using Ecommerce.StockService.Models;
using Ecommerce.StockService.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecommerce.StockService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductServer _productServer;

        public ProductsController(ProductServer productServer)
        {
            _productServer = productServer;
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] ProductRequest productRequest)
        {
            try
            {
                var productResponse = await _productServer.CreateAsync(productRequest);

                return CreatedAtAction(nameof(GetProductByIdAsync), new { id = productResponse.Id }, productResponse);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product?>> GetProductByIdAsync(int id)
        {
            var product = await _productServer.GetByIdAsync(id);

            if (product == null) { return NotFound(); }

            return Ok(product);
        }
    }
}
