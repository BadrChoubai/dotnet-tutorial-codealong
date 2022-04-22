using ECommerce.Entities;
public interface IProductsRepository
{
    Task<Product> GetProductAsync(Guid id);
    Task<IEnumerable<Product>> GetProductsAsync();
    Task CreateProductAsync(Product newProduct);
    Task UpdateProductAsync(Product updatedProduct);
    Task DeleteProductAsync(Guid id);
    
}