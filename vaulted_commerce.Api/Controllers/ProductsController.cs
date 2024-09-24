// API/Controllers/ProductController.cs
using Microsoft.AspNetCore.Mvc;
using vaulted_commerce.Framework.Services; // Access ProductService from Framework
using vaulted_commerce.DataAccessLayer.Entities;
using vaulted_commerce.Framework.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vaulted_commerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
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
public async Task<ActionResult<Product>> Create([FromBody] ProductDto productDto)
{
    try
    {
        // Call the service to create the product
        var createdProduct = await _productService.CreateProductAsync(productDto);
        
        // Return a 201 Created response with the new product
        return CreatedAtRoute("GetProduct", new { id = createdProduct.Id }, createdProduct);
    }
    catch (ArgumentNullException ex)
    {
        // Handle the case where productDto is null
        return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
        // Handle any other exceptions
        return StatusCode(500, "Internal server error: " + ex.Message);
    }
}


        // PUT: api/Product
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Update(string id, [FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Product data is required.");
            }

            try
            {
                await _productService.UpdateProductAsync(id, productDto);
                return NoContent();  // Return 204 No Content if the update is successful
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
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
