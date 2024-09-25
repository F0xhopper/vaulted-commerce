using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace vaulted_commerce.DataAccessLayer.Entities
{
    public class Order
    {
        [BsonId]  // Marks this property as the primary key in MongoDB
        [BsonRepresentation(BsonType.ObjectId)]  // Handles conversion between ObjectId and string
        public string Id { get; set; }

        // Represents the date and time when the order was created
        public DateTime OrderDate { get; set; }

        // Represents the total amount for the order
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal TotalAmount { get; set; }

        // Represents the customer's ID who placed the order
        public string CustomerId { get; set; }

        // Optional: Guest's name for guest orders
        public string GuestName { get; set; }

        // Optional: Guest's email for guest orders
        public string GuestEmail { get; set; }

        // Represents the shipping address for the order
        public ShippingAddress ShippingAddress { get; set; }

        // A list of products associated with this order
        public List<OrderProduct> Products { get; set; } = new List<OrderProduct>(); // Initializes with an empty list

        // Optional: Status of the order (e.g., Pending, Completed, Canceled)
        public string Status { get; set; }
    }

    public class OrderProduct
    {
        // Represents the product ID
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }

        // Quantity of the product ordered
        public int Quantity { get; set; }
        
        public decimal Price { get; set; }

    }

    public class ShippingAddress
    {
        // Street address
        public string Street { get; set; }

        // City
        public string City { get; set; }

        
        // Postal/ZIP code
        public string PostalCode { get; set; }

        // Country
        public string Country { get; set; }
    }
}
