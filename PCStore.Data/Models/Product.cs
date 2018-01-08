namespace PCStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        [MinLength(2)]
        public string Make { get; set; }

        [Required]
        [StringLength(30)]
        [MinLength(2)]
        public string Model { get; set; }

        public byte[] Photo { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [MinLength(2)]
        public string Type { get; set; }

        [Required]
        [MinLength(5)]
        public string Specs { get; set; }

        public List<ProductBuyer> Buyers { get; } = new List<ProductBuyer>();
    }
}
