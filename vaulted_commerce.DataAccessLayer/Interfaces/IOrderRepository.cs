using vaulted_commerce.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vaulted_commerce.DataAccessLayer.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(string id);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(string id);
        Task<List<Order>> GetByIdsAsync(IEnumerable<string> ids);
        Task DeleteManyAsync(IEnumerable<string> ids);
    }
}
