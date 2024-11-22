namespace Ecommerce.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? Type { get; set; }
        public string? short_description { get; set; }
        public string? Dimensions { get; set; }
        public decimal? Price { get; set; }
        public int? StockQuantity { get; set; }
        public bool? IsVisible { get; set; }
        public string? Brand { get; set; }
        public string? image_url { get; set; }
        public string? LongDescription { get; set; }
        public bool? IsFeatured { get; set; }
    }
}
