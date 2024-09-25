// DataAccessLayer/Repositories/ProductRepository.cs
using MongoDB.Driver;
using vaulted_commerce.DataAccessLayer.Entities;
using vaulted_commerce.DataAccessLayer.Context;
using System.Collections.Generic;
using System.Linq; 
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

        public async Task AddManyAsync(IEnumerable<Product> products)
        {
            await _products.InsertManyAsync(products); // Insert multiple products at once
        }

        public async Task UpdateAsync(Product product)
        {
            await _products.ReplaceOneAsync(p => p.Id == product.Id, product);
        }

        public async Task UpdateManyAsync(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                await UpdateAsync(product); // Update each product
            }
        }

        public async Task DeleteAsync(string id)
        {
            await _products.DeleteOneAsync(product => product.Id == id);
        }

        public async Task DeleteManyAsync(IEnumerable<string> ids)
        {
            foreach (var id in ids)
            {
                await DeleteAsync(id); // Delete each product by id
            }
        }

        // New method to get products by multiple IDs
         public async Task<List<Product>> GetByIdsAsync(IEnumerable<string> ids)
        {
            return await _products.Find(product => ids.Contains(product.Id)).ToListAsync();
        }
    }
}
