// API/Controllers/OrderController.cs
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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // POST: api/order
       [HttpPost]
        public async Task<ActionResult<string>> Create([FromBody] OrderDto orderDto)
        {
            try
            {
                var createdOrder = await _orderService.CreateOrderAsync(orderDto);
                return Created("", createdOrder.Id);  // Return 201 Created with just the ID
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



        // PUT: api/order/{id}
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] OrderDto orderDto)
        {
            if (orderDto == null)
            {
                return BadRequest("Order data is required.");
            }

            try
            {
                await _orderService.UpdateOrderAsync(orderDto);
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

      
        // DELETE: api/order/{id}
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }

        // DELETE: api/order/bulk
        [HttpDelete("bulk")]
        public async Task<IActionResult> DeleteMultiple([FromBody] IEnumerable<string> ids)
        {
            await _orderService.DeleteMultipleOrdersAsync(ids);
            return NoContent();  // Return 204 No Content if the deletes are successful
        }
    }
}
