namespace Ecommerce.Models
{
    public class CartItemModel
    {
        public int Id {get; set;}
        public int product_id { get; set; }
        public string? UserID { get; set; }
        public int quantity { get; set; }
        public decimal Price { get; set; } 
        public string? Brand {get; set;}
        public string? Name {get; set;}
        public string? image_url {get; set;}

    }
}
