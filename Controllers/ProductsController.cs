using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ECommerce.Dtos;
using ECommerce.Entities;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository repository;

        // Constructor
        public ProductsController(IProductsRepository repository)
        {
            this.repository = repository;
        }

        // GET /products
        [HttpGet]
       public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = (await repository.GetProductsAsync())
                .Select(product => product.AsDto());
            return products;
        }

        // GET /products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductAsync(Guid id)
        {
            var product = await repository.GetProductAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            return product.AsDto();
        }

        // POST /products
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProductAsync(CreateProductDto productDto)
        {
            Product newProduct = new()
            {
                Id = Guid.NewGuid(),
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                CreatedDate = DateTimeOffset.UtcNow,
            };

            await repository.CreateProductAsync(newProduct);

            return CreatedAtAction(nameof(GetProductAsync), new { id = newProduct.Id }, newProduct.AsDto());
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProductAsync(Guid id, UpdateProductDto productDto)
        {
            var existingProduct = await repository.GetProductAsync(id);

            if (existingProduct is null)
            {
                return NotFound();
            }

            Product updatedProduct = existingProduct with 
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
            };

            await repository.UpdateProductAsync(updatedProduct);
            return NoContent();
        }

        // DELETE /products/{id}
        [HttpDelete("{id}")]
        public async  Task<ActionResult> DeleteProductAsync(Guid id)
        {
            var existingProduct = await repository.GetProductAsync(id);

            if (existingProduct is null)
            {
                return NotFound();
            }

            await repository.DeleteProductAsync(id);
            return NoContent();
        }
    }
}