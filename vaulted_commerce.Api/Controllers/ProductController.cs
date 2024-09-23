// API/Controllers/ProductController.cs
using Microsoft.AspNetCore.Mvc;
using vaulted_commerce.Framework.Services; // Access ProductService from Framework
using vaulted_commerce.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vaulted_commerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // GET: api/Product/{id}
        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            await _productService.CreateProductAsync(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id.ToString() }, product);
        }

        // PUT: api/Product
        [HttpPut]
        public async Task<IActionResult> Update(Product updatedProduct)
        {
            // Get the product by the ID from the body of the updatedProduct
            var product = await _productService.GetProductByIdAsync(updatedProduct.Id);

            if (product == null)
            {
                return NotFound();
            }

            // Update the product without manually setting the ID, as it's already part of updatedProduct
            await _productService.UpdateProductAsync(updatedProduct);
            return NoContent();
        }


        // DELETE: api/Product/{id}
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
