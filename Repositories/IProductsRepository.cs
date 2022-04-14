using ECommerce.Entities;
public interface IProductsRepository
{
    Product GetProduct(Guid id);
    IEnumerable<Product> GetProducts();
    void CreateProduct(Product newProduct);
    void UpdateProduct(Product updatedProduct);
    void DeleteProduct(Guid id);
    
}