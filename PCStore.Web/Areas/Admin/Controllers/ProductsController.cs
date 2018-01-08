namespace PCStore.Web.Areas.Admin.Controllers
{
    using PCStore.Data;
    using PCStore.Data.Models;
    using PCStore.Web.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using PCStore.Web.Areas.Admin.Models.Products;
    using PCStore.Services.Admin;
    using PCStore.Services.Admin.Implementations;

    [Area("Admin")]
    [Authorize(Roles = WebConstants.AdministratorRole)]
    public class ProductsController : Controller
    {        
        private readonly IAdminProductService products;

        public ProductsController(IAdminProductService products)
        {
            this.products = products;
        }

        [HttpGet]
        public IActionResult Add() => View();        

        [HttpPost]
        public async Task<IActionResult> Add(Product product, IFormFile Photo)
        {
            var result = await products.AddProduct(product, Photo);            
            TempData.AddSuccessMessage(result);

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Display()
        {
            var products = await this.products.AllAsync();

            return View(new ProductListingViewModel
            {
                Products = products
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await this.products.DeleteProduct(id);
            TempData.AddSuccessMessage(result);

            return RedirectToAction(nameof(Display));
        }
                
        public async Task<IActionResult> EditForm(int id)
        {
            var product = await this.products.ByIdAsync<EditModel>(id);

            return View(product);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Make, Model, Quantity, Price, Specs")] Product product)
        {
            var result = await products.EditProduct(id, product);
            TempData.AddSuccessMessage(result);

            return RedirectToAction(nameof(Display));
        }
    }
}
