// DataAccessLayer/Repositories/ProductRepository.cs
using MongoDB.Driver;
using vaulted_commerce.DataAccessLayer.Entities;
using vaulted_commerce.DataAccessLayer.Context;
using vaulted_commerce.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vaulted_commerce.DataAccessLayer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepository(MongoDbContext context)
        {
            _products = context.Products;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _products.Find(product => true).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await _products.Find(product => product.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _products.InsertOneAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            await _products.ReplaceOneAsync(p => p.Id == product.Id, product);
        }


        public async Task DeleteAsync(string id)
        {
            await _products.DeleteOneAsync(product => product.Id == id);
        }
    }
}
