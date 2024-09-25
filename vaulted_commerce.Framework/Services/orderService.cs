using vaulted_commerce.DataAccessLayer.Entities;
using vaulted_commerce.DataAccessLayer.Repositories;
using vaulted_commerce.Framework.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vaulted_commerce.Framework.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;  // Injecting ProductRepository

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;  // Initialize ProductRepository
        }

        public async Task<Order> CreateOrderAsync(OrderDto orderDto)
        {
            if (orderDto == null)
            {
                throw new ArgumentNullException(nameof(orderDto));
            }

            var orderProducts = new List<OrderProduct>();

            foreach (var productDto in orderDto.Products)
            {
                // Fetch the product by its ID
                var product = await _productRepository.GetByIdAsync(productDto.ProductId);
                
                if (product == null)
                {
                    throw new KeyNotFoundException($"Product with ID {productDto.ProductId} not found.");
                }

                // Check stock availability and reduce stock
                var stockReduced = await ReduceStockAsync(productDto.ProductId, productDto.Quantity);
                
                if (!stockReduced)
                {
                    throw new InvalidOperationException($"Not enough stock for product {productDto.ProductId}. Requested: {productDto.Quantity}, Available: {product.Stock}");
                }

                // Add product details to the order
                orderProducts.Add(new OrderProduct
                {
                    ProductId = productDto.ProductId,
                    Quantity = productDto.Quantity,
                    Price = product.Price // Storing price at the time of order
                });
            }

            // Create the order entity
            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                CustomerId = orderDto.CustomerId,  // Optional, can be null for guest orders
                GuestName = orderDto.GuestName,    // Optional, for guest orders
                GuestEmail = orderDto.GuestEmail,  // Optional, for guest orders
                ShippingAddress = new ShippingAddress
                {
                    Street = orderDto.ShippingAddress.Street,
                    City = orderDto.ShippingAddress.City,
                    PostalCode = orderDto.ShippingAddress.PostalCode,
                    Country = orderDto.ShippingAddress.Country
                },
                Products = orderProducts,
                TotalAmount = orderDto.TotalAmount,  // Calculated total
                Status = "Pending"
            };

            // Save the order to the repository
            await _orderRepository.AddAsync(order);

            return order; // Return the created order object
        }

        public async Task<bool> ReduceStockAsync(string productId, int quantity)
        {
            // Fetch the product by its ID
            var product = await _productRepository.GetByIdAsync(productId);
            
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {productId} not found.");
            }

            // Check if there's enough stock to fulfill the order
            if (product.Stock < quantity)
            {
                return false; // Insufficient stock
            }

            // Reduce the stock
            product.Stock -= quantity;

            // Update the product in the repository
            await _productRepository.UpdateAsync(product);

            return true; // Stock reduced successfully
        }

        public async Task<Order> GetOrderByIdAsync(string id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task UpdateOrderAsync(OrderDto orderDto)
        {
            if (orderDto == null)
                throw new ArgumentNullException(nameof(orderDto));

            var existingOrder = await _orderRepository.GetByIdAsync(orderDto.Id);
            if (existingOrder == null)
                throw new KeyNotFoundException($"Order with ID {orderDto.Id} not found.");

            // Map updated properties
            existingOrder.TotalAmount = orderDto.TotalAmount;
            existingOrder.CustomerId = orderDto.CustomerId;
            existingOrder.GuestName = orderDto.GuestName; // Update guest name if provided
            existingOrder.GuestEmail = orderDto.GuestEmail; // Update guest email if provided
            existingOrder.ShippingAddress = new ShippingAddress
            {
                Street = orderDto.ShippingAddress.Street,
                City = orderDto.ShippingAddress.City,
                PostalCode = orderDto.ShippingAddress.PostalCode,
                Country = orderDto.ShippingAddress.Country
            };
            existingOrder.Products = orderDto.Products.Select(p => new OrderProduct
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity,
            }).ToList();
            existingOrder.Status = orderDto.Status; // Update status if provided

            await _orderRepository.UpdateAsync(existingOrder);
        }

        public async Task DeleteOrderAsync(string id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }

            await _orderRepository.DeleteAsync(id);
        }

        public async Task DeleteMultipleOrdersAsync(IEnumerable<string> ids)
        {
            var orders = await _orderRepository.GetByIdsAsync(ids);
            if (orders.Count() != ids.Count())
            {
                throw new KeyNotFoundException("One or more orders not found."); // Throw if any of the orders do not exist
            }

            await _orderRepository.DeleteManyAsync(ids);
        }
    }
}
