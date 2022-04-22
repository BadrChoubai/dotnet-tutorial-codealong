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
        public async Task CreateProductAsync(Product newProduct)
        {
            await productsCollection.InsertOneAsync(newProduct);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var filter = filterBuilder.Eq(product => product.Id, id);
            await productsCollection.DeleteOneAsync(filter);
        }

        public async Task<Product> GetProductAsync(Guid id)
        {
            var filter = filterBuilder.Eq(product => product.Id, id);
            return await productsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async  Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await productsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateProductAsync(Product updatedProduct)
        {
            var filter = filterBuilder.Eq(existingProduct => existingProduct.Id,updatedProduct.Id);
            await productsCollection.ReplaceOneAsync(filter, updatedProduct);
        }
    }
}