// API/Controllers/ProductsController.cs
using Microsoft.AspNetCore.Mvc;
using vaulted_commerce.Framework.Services; 
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

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> Create([FromBody] ProductDto productDto)
        {
            try
            {
                var createdProduct = await _productService.CreateProductAsync(productDto);
                return CreatedAtRoute("GetProduct", new { id = createdProduct.Id }, createdProduct);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // POST: api/Product/bulk
        [HttpPost("bulk")]
        public async Task<ActionResult> CreateMultiple([FromBody] IEnumerable<ProductDto> productDtos)
        {
            try
            {
                // Add products to the database
                await _productService.AddMultipleProductsAsync(productDtos);

                // Return 201 Created status with a success message
                return StatusCode(201, "Products created successfully.");
            }
            catch (Exception ex)
            {
                // Return 500 status with error message in case of an exception
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


        // PUT: api/Product/{id}
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Product data is required.");
            }

            try
            {
                await _productService.UpdateProductAsync(productDto);
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

        // PUT: api/Product/bulk
        [HttpPut("bulk")]
        public async Task<ActionResult> UpdateMultiple([FromBody] IEnumerable<ProductDto> productDtos)
        {
            if (productDtos == null)
            {
                return BadRequest("Product data is required.");
            }

            try
            {
                await _productService.UpdateMultipleProductsAsync(productDtos);
                return NoContent();  // Return 204 No Content if the updates are successful
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
    try
    {
        await _productService.DeleteProductAsync(id);
        return NoContent(); // Return 204 No Content if delete is successful
    }
    catch (KeyNotFoundException)
    {
        return NotFound(); // Return 404 Not Found if the product does not exist
    }
    catch (Exception ex)
    {
        return StatusCode(500, "Internal server error: " + ex.Message); // Return 500 for any other errors
    }
}

// DELETE: api/Product/bulk
[HttpDelete("bulk")]
public async Task<IActionResult> DeleteMultiple([FromBody] IEnumerable<string> ids)
{
    try
    {
        await _productService.DeleteMultipleProductsAsync(ids);
        return NoContent(); // Return 204 No Content if the deletes are successful
    }
    catch (KeyNotFoundException)
    {
        return NotFound(); // Return 404 Not Found if one or more products do not exist
    }
    catch (Exception ex)
    {
        return StatusCode(500, "Internal server error: " + ex.Message); // Return 500 for any other errors
    }
}


    }
}
