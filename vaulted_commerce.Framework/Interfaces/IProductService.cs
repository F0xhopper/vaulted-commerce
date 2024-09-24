// Framework/Services/IProductService.cs
using vaulted_commerce.DataAccessLayer.Entities;
using vaulted_commerce.Framework.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vaulted_commerce.Framework.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(string id);
        Task<Product> CreateProductAsync(ProductDto productDto);
        Task UpdateProductAsync(string id, ProductDto productDto);
        Task DeleteProductAsync(string id);
    }
}
