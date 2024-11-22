namespace Ecommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public string ShortDescription { get; set; }
        public string Dimensions { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string Brand { get; set; }
        public string? LongDescription { get; set; }
    }
}
