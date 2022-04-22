using ECommerce.Entities;


namespace ECommerce.Repositories
{
    public class InMemProductsRepository : IProductsRepository
    {
        private readonly List<Product> products = new()
        {
            new Product { Id = Guid.NewGuid(), Name = "Men's Sweater", Description = "Lorem ipsum dolor sit amet", Price = 119,  CreatedDate = DateTimeOffset.UtcNow },
            new Product { Id = Guid.NewGuid(), Name = "Men's Down Jacket", Description = "Lorem ipsum dolor sit amet", Price = 199,  CreatedDate = DateTimeOffset.UtcNow },
            new Product { Id = Guid.NewGuid(), Name = "Men's Rain Jacket", Description = "Lorem ipsum dolor sit amet", Price = 79,  CreatedDate = DateTimeOffset.UtcNow },
        };


        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await Task.FromResult(products);
        }

        public async Task<Product> GetProductAsync(Guid id)
        {
            var item = products.Where(item => item.Id == id).SingleOrDefault();
            return await Task.FromResult(item);
        }

        public async Task CreateProductAsync(Product newProduct)
        {
            products.Add(newProduct);
            await Task.CompletedTask;
        }

        public async Task UpdateProductAsync(Product updatedProduct)
        {
            var index = products.FindIndex(existingProduct => existingProduct.Id == updatedProduct.Id);
            products[index] = updatedProduct;
            await Task.CompletedTask;
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var index = products.FindIndex(product => product.Id == id);
            products.RemoveAt(index);
            await Task.CompletedTask;
        }
    }

}

