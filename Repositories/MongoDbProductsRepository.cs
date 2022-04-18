using ECommerce.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ECommerce.Repositories
{
    public class MongoDbProductsRepository : IProductsRepository
    {
        private const string databaseName = "ecommerce";
        private const string collectionName = "products";
        private readonly IMongoCollection<Product> productsCollection;
        private readonly FilterDefinitionBuilder<Product> filterBuilder = Builders<Product>.Filter;
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
            var filter = filterBuilder.Eq(product => product.Id, id);
            productsCollection.DeleteOne(filter);
        }

        public Product GetProduct(Guid id)
        {
            var filter = filterBuilder.Eq(product => product.Id, id);
            return productsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Product> GetProducts()
        {
            return productsCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateProduct(Product updatedProduct)
        {
            var filter = filterBuilder.Eq(existingProduct => existingProduct.Id,updatedProduct.Id);
            productsCollection.ReplaceOne(filter, updatedProduct);
        }
    }
}