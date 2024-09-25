namespace vaulted_commerce.Framework.DTOs
{
    public class OrderDto
    {
        public string Id { get; set; } // Unique identifier for the order
        public string CustomerId { get; set; } // Can be null for guest orders
        public string GuestName { get; set; } // Optional, to store guest name
        public string GuestEmail { get; set; } // Optional, to store guest email
        public ShippingAddressDto ShippingAddress { get; set; } // Shipping address details
        public List<OrderProductDto> Products { get; set; } // List of products being ordered
        public string Status { get; set; } // Status of the order (e.g., Pending, Completed)
        public decimal TotalAmount { get; set; } // Total amount of the order
    }

    public class OrderProductDto
    {
        public string ProductId { get; set; } // Product ID
        public int Quantity { get; set; } // Quantity ordered

    }

    public class ShippingAddressDto
    {
        public string Street { get; set; } // Street address
        public string City { get; set; } // City
        public string PostalCode { get; set; } // Postal/ZIP code
        public string Country { get; set; } // Country
    }
}