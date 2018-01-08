namespace PCStore.Web.Areas.Admin.Models.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddProductViewModel : IValidatableObject
    {
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Make.Length < 3 || this.Make == null)
            {
                yield return new ValidationResult("The Make field value must be atleast 3 symbols long.");
            }
            if (this.Model.Length < 3 || this.Model == null)
            {
                yield return new ValidationResult("The Model field value must be atleast 3 symbols long.");
            }
            if (this.Quantity < 1)
            {
                yield return new ValidationResult("The Quantity must be atleast 1 or higher.");
            }
            if (this.Price <= 0)
            {
                yield return new ValidationResult("The Price must be greater than 0.");
            }            
            if (this.Specs.Length < 2 || this.Specs == null)
            {
                yield return new ValidationResult("The Specs field value must be atleast 5 symbols long.");
            }
            if (this.Photo.Length <= 0)
            {
                yield return new ValidationResult("The Photo field is empty.");
            }
        }
    }
}