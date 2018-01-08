namespace PCStore.Web.Models.Store
{
    using System.ComponentModel.DataAnnotations;

    public class ProductDetailsViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        [MinLength(2)]
        public string Make { get; set; }

        [Required]
        [StringLength(30)]
        [MinLength(2)]
        public string Model { get; set; }

        [Required]
        public byte[] Photo { get; set; }

        [Required]
        [Range(0, 10000)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, 10000)]
        public decimal Price { get; set; }

        [Required]
        [MinLength(5)]
        public string Specs { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
