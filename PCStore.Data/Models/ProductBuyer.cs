namespace PCStore.Data.Models
{
    public class ProductBuyer
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
