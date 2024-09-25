using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System; 

namespace vaulted_commerce.DataAccessLayer.Entities
{
    public class Product
    {
        [BsonId]  // Marks this property as the primary key in MongoDB
        [BsonRepresentation(BsonType.ObjectId)]  // Handles conversion between ObjectId and string
        public string Id { get; set; }

        public string Name { get; set; }

        // BsonRepresentation(BsonType.Decimal128) stores the decimal value as MongoDB's Decimal128 type.
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }

        // Use List<string> instead of array to align with MongoDB's handling of array-like structures.
        public List<string> Categories { get; set; }

        public int Stock { get; set; }
    }
}
