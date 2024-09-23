// Framework/Services/IProductService.cs
using vaulted_commerce.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vaulted_commerce.Framework.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(string id);
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(string id);
    }
}
