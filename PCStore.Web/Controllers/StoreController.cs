namespace PCStore.Web.Areas.Store.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PCStore.Services.Admin;
    using PCStore.Web.Areas.Admin.Models.Products;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using PCStore.Services.Admin.Models;
    using PCStore.Web.Models.Store;

    public class StoreController : Controller
    {
        private readonly IAdminProductService products;        

        public StoreController(IAdminProductService products)
        {
            this.products = products;
        }

        public async Task<IActionResult> AllProducts()
        {
            var products = await this.products.AllAsync();

            return View(new ProductListingViewModel
            {
                Products = products
            });
        }      


        public async Task<IActionResult> CPU()
        {
            var products = await this.products.AllAsync();
            
            return this.View(new ProductListingViewModel
               {
                   Products = products.Where(x => x.Type == "CPU")
               });
        }

        public async Task<IActionResult> GPU()
        {
            var products = await this.products.AllAsync();

            return this.View(new ProductListingViewModel
            {
                Products = products.Where(x => x.Type == "GPU")
            });
        }

        public async Task<IActionResult> PSU()
        {
            var products = await this.products.AllAsync();

            return this.View(new ProductListingViewModel
            {
                Products = products.Where(x => x.Type == "PSU")
            });
        }

        public async Task<IActionResult> RAM()
        {
            var products = await this.products.AllAsync();

            return this.View(new ProductListingViewModel
            {
                Products = products.Where(x => x.Type == "RAM")
            });
        }

        public async Task<IActionResult> Motherboard()
        {
            var products = await this.products.AllAsync();

            return this.View(new ProductListingViewModel
            {
                Products = products.Where(x => x.Type == "Motherboard")
            });
        }

        public async Task<IActionResult> StorageDevice()
        {
            var products = await this.products.AllAsync();

            return this.View(new ProductListingViewModel
            {
                Products = products.Where(x => x.Type == "DiskDrive")
            });
        }

        public async Task<IActionResult> PCCase()
        {
            var products = await this.products.AllAsync();

            return this.View(new ProductListingViewModel            
            {
                Products = products.Where(x => x.Type == "PCCase")
            });
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await this.products.ByIdAsync<ProductDetailsViewModel>(id);

            return View(product);
        }        
    }
}