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


        public IEnumerable<Product> GetProducts()
        {
            return products;
        }

        public Product GetProduct(Guid id)
        {
            return products.Where(item => item.Id == id).SingleOrDefault();
        }

        public void CreateProduct(Product newProduct)
        {
            products.Add(newProduct);
        }

        public void UpdateProduct(Product updatedProduct)
        {
            var index = products.FindIndex(existingProduct => existingProduct.Id == updatedProduct.Id);
            products[index] = updatedProduct;
        }

        public void DeleteProduct(Guid id)
        {
            var index = products.FindIndex(product => product.Id == id);
            products.RemoveAt(index);
        }
    }

}

