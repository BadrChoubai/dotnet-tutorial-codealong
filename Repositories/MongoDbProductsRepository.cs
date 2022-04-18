using ECommerce.Entities;
using MongoDB.Driver;

namespace ECommerce.Repositories
{
    public class MongoDbProductsRepository : IProductsRepository
    {
        private const string databaseName = "ecommerce";
        private const string collectionName = "products";
        private readonly IMongoCollection<Product> productsCollection;
        public MongoDbProductsRepository(IMongoClient mongoClient) 
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            productsCollection = database.GetCollection<Product>(databaseName);
        }
        public void CreateProduct(Product newProduct)
        {
            productsCollection.InsertOne(newProduct);
        }

        public void DeleteProduct(Guid id)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProducts()
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product updatedProduct)
        {
            throw new NotImplementedException();
        }
    }
}