namespace Ecommerce.Models
{
    public class Transaction
    {
        public string payment_id { get; set; }
        public string payment_method { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }

}
