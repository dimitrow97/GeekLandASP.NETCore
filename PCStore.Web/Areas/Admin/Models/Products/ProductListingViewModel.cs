namespace PCStore.Web.Areas.Admin.Models.Products
{    
    using PCStore.Services.Admin.Models;
    using System.Collections.Generic;

    public class ProductListingViewModel
    {
        public IEnumerable<AdminProductListingServiceModel> Products { get; set; }
    }
}
