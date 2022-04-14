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
       public IEnumerable<ProductDto> GetProducts()
        {
            var products = repository.GetProducts().Select(product => product.AsDto());
            return products;
        }

        // GET /products/{id}
        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetProduct(Guid id)
        {
            var product = repository.GetProduct(id);

            if (product is null)
            {
                return NotFound();
            }

            return product.AsDto();
        }

        // POST /products
        [HttpPost]
        public ActionResult<ProductDto> CreateProduct(CreateProductDto productDto)
        {
            Product newProduct = new()
            {
                Id = Guid.NewGuid(),
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                CreatedDate = DateTimeOffset.UtcNow,
            };

            repository.CreateProduct(newProduct);

            return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct.AsDto());
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(Guid id, UpdateProductDto productDto)
        {
            var existingProduct = repository.GetProduct(id);

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

            repository.UpdateProduct(updatedProduct);
            return NoContent();
        }

        // DELETE /products/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(Guid id)
        {
            var existingProduct = repository.GetProduct(id);

            if (existingProduct is null)
            {
                return NotFound();
            }

            repository.DeleteProduct(id);
            return NoContent();
        }
    }
}