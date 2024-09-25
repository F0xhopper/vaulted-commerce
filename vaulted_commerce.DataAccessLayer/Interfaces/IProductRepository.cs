// DataAccessLayer/Repositories/IProductRepository.cs
using vaulted_commerce.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vaulted_commerce.DataAccessLayer.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(string id);
        Task<List<Product>> GetByIdsAsync(IEnumerable<string> ids); 

        Task AddAsync(Product product);
        Task AddManyAsync(IEnumerable<Product> products); // New method for adding multiple products
        Task UpdateAsync(Product product);
        Task UpdateManyAsync(IEnumerable<Product> products); // New method for updating multiple products
        Task DeleteAsync(string id);
        Task UpdateStockAsync(string productId, int newStock);
        Task DeleteManyAsync(IEnumerable<string> ids); // New method for deleting multiple products
    }
}
