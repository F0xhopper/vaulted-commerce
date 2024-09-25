using vaulted_commerce.DataAccessLayer.Entities;
using vaulted_commerce.Framework.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vaulted_commerce.Framework.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(OrderDto orderDto);
        Task<Order> GetOrderByIdAsync(string id);
        Task UpdateOrderAsync(OrderDto orderDto);
        Task<bool> UpdateStockAsync(string productId, int quantityChange);
        Task DeleteOrderAsync(string id);

        Task DeleteMultipleOrdersAsync(IEnumerable<string> ids);
        // Additional methods can be added as needed
    }
}
