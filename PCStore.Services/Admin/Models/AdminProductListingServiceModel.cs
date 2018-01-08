namespace PCStore.Services.Admin.Models
{
    using Common.Mapping;
    using Data.Models;

    public class AdminProductListingServiceModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public byte[] Photo { get; set; }

        public string Type { get; set; }

        public string Specs { get; set; }
    }
}
