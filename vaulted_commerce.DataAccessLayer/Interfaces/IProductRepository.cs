using vaulted_commerce.DataAccessLayer.Entities;
using System.Collections.Generic; // For IEnumerable<>
using System.Threading.Tasks;
namespace vaulted_commerce.DataAccessLayer.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(string id);
        Task<List<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(string id);
    }
}
