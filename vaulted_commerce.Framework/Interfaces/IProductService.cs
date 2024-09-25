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
        Task AddMultipleProductsAsync(IEnumerable<ProductDto> productDtos); // New method for adding multiple products
        Task UpdateProductAsync(ProductDto productDto);
        Task UpdateMultipleProductsAsync(IEnumerable<ProductDto> productDtos); // New method for updating multiple products
        Task DeleteProductAsync(string id);
        Task DeleteMultipleProductsAsync(IEnumerable<string> ids); // New method for deleting multiple products
    }
}