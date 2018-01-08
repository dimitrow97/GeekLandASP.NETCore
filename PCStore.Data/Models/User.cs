namespace PCStore.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public string Name { get; set; }

        public string Adress { get; set; }

        public List<ProductBuyer> ProductsBougth { get; } = new List<ProductBuyer>();
    }
}