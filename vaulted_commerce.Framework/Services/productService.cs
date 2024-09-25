// Framework/Services/ProductService.cs
using vaulted_commerce.DataAccessLayer.Entities;
using vaulted_commerce.DataAccessLayer.Repositories;
using vaulted_commerce.Framework.DTOs;
using System.Collections.Generic;
using System.Linq; // Ensure to include this for Count()
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
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                CreatedAt = DateTime.UtcNow,
                Categories = productDto.Categories,
                Stock = productDto.Stock
            };

            await _productRepository.AddAsync(product);
            return product; // Return the created product
        }

        public async Task AddMultipleProductsAsync(IEnumerable<ProductDto> productDtos)
        {
            var products = new List<Product>();
            foreach (var dto in productDtos)
            {
                products.Add(new Product
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    CreatedAt = DateTime.UtcNow,
                    Categories = dto.Categories,
                    Stock = dto.Stock
                });
            }

            await _productRepository.AddManyAsync(products); // Add multiple products
        }

        public async Task UpdateProductAsync(ProductDto productDto)
        {
            var product = new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Price = productDto.Price,
                Categories = productDto.Categories,
                Stock = productDto.Stock
            };

            await _productRepository.UpdateAsync(product);
        }

        public async Task UpdateMultipleProductsAsync(IEnumerable<ProductDto> productDtos)
        {
            var products = new List<Product>();
            foreach (var dto in productDtos)
            {
                var product = new Product
                {
                    Id = dto.Id, // Assuming ProductDto has an Id property
                    Name = dto.Name,
                    Price = dto.Price,
                    Categories = dto.Categories,
                    Stock = dto.Stock
                };

                products.Add(product);
            }

            await _productRepository.UpdateManyAsync(products); // Update multiple products
        }

        // Now throws exceptions if the product is not found
        public async Task DeleteProductAsync(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found."); // Throw an exception if the product does not exist
            }

            await _productRepository.DeleteAsync(id);
        }

        // Delete multiple products with existence check
        public async Task DeleteMultipleProductsAsync(IEnumerable<string> ids)
        {
            var products = await _productRepository.GetByIdsAsync(ids);

            if (products.Count() != ids.Count())
            {
                throw new KeyNotFoundException("One or more products not found."); // Throw an exception if any of the products do not exist
            }

            await _productRepository.DeleteManyAsync(ids);
        }
    }
}
