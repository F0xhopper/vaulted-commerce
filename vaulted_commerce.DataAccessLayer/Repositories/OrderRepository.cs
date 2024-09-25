using MongoDB.Driver;
using vaulted_commerce.DataAccessLayer.Entities;
using vaulted_commerce.DataAccessLayer.Context;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;

namespace vaulted_commerce.DataAccessLayer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderRepository(MongoDbContext context)
        {
            _orders = context.Orders; // Assuming Orders collection is defined in MongoDbContext
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _orders.Find(order => true).ToListAsync(); // Fetch all orders
        }

        public async Task<Order> GetByIdAsync(string id)
        {
            return await _orders.Find(order => order.Id == id).FirstOrDefaultAsync(); // Fetch order by ID
        }

        public async Task AddAsync(Order order)
        {
            await _orders.InsertOneAsync(order); // Insert a single order
        }

        public async Task UpdateAsync(Order order)
        {
            await _orders.ReplaceOneAsync(o => o.Id == order.Id, order); // Update an existing order
        }

        public async Task DeleteAsync(string id)
        {
            await _orders.DeleteOneAsync(order => order.Id == id); // Delete an order by ID
        }

        public async Task<List<Order>> GetByIdsAsync(IEnumerable<string> ids)
        {
            return await _orders.Find(order => ids.Contains(order.Id)).ToListAsync(); // Get orders by multiple IDs
        }

        public async Task DeleteManyAsync(IEnumerable<string> ids)
        {
            foreach (var id in ids)
            {
                await DeleteAsync(id); // Delete each order by ID
            }
        }
    }
}
