using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PCStore.Services.Admin;
using PCStore.Web.Areas.Admin.Models.Products;
using PCStore.Web.Models;

namespace PCStore.Web.Controllers
{
    public class HomeController : Controller
    {        
        private readonly IAdminProductService products;

        public HomeController(IAdminProductService products)
        {
            this.products = products;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await this.products.AllAsyncRandomHome();

            return View(new ProductListingViewModel
            {
                Products = products
            });
        }
            
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
