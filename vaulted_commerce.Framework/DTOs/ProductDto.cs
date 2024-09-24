// vaulted_commerce.Framework.DTOs/ProductDto.cs
namespace vaulted_commerce.Framework.DTOs
{
    public class ProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<string> Categories { get; set; }
        public int Stock { get; set; }
    }
}
