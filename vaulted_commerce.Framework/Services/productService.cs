// Framework/Services/ProductService.cs
using vaulted_commerce.DataAccessLayer.Entities;
using vaulted_commerce.DataAccessLayer.Repositories;
using vaulted_commerce.Framework.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vaulted_commerce.Framework.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

    public async Task<Product> CreateProductAsync(ProductDto productDto)
{
    if (productDto == null)
    {
        throw new ArgumentNullException(nameof(productDto), "Product data cannot be null.");
    }

    // Convert DTO to Product
    var product = new Product
    {
        Name = productDto.Name,
        Price = productDto.Price,
        Categories = productDto.Categories,
        Stock = productDto.Stock
    };

    // Add the product to the repository
    await _productRepository.AddAsync(product);

    // Return the created product
    return product; // This will contain the generated Id
}


    public async Task UpdateProductAsync(string id, ProductDto productDto)
    {
        // Retrieve the existing product by id
        var existingProduct = await _productRepository.GetByIdAsync(id);
     

        // Update the properties of the existing product with values from the DTO
        existingProduct.Name = productDto.Name;
        existingProduct.Price = productDto.Price;
        existingProduct.Categories = productDto.Categories;
        existingProduct.Stock = productDto.Stock;

        // Save the updated product back to the repository
        await _productRepository.UpdateAsync(existingProduct);
    }



        public async Task DeleteProductAsync(string id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}
