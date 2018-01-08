namespace PCStore.Web.Models.ShoppingCart
{
    public class CartItemViewModel
    {
        public int ProductId { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public byte[] Photo { get; set; }
    }
}
