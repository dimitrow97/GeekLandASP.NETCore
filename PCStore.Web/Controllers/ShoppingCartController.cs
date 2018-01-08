namespace PCStore.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PCStore.Data;
    using PCStore.Services.Admin;
    using PCStore.Services.ShoppingCart;
    using PCStore.Web.Infrastructure.Extensions;
    using System.Linq;
    using PCStore.Services.ShoppingCart.Models;
    using Microsoft.AspNetCore.Authorization;
    using PCStore.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService cartService;
        private readonly IAdminProductService productService;
        private readonly UserManager<User> userService;
        private readonly PCStoreDbContext db;

        public ShoppingCartController(IShoppingCartService cartService, IAdminProductService productService, PCStoreDbContext db, UserManager<User> userService)
        {
            this.cartService = cartService;
            this.productService = productService;
            this.db = db;
            this.userService = userService;
        }

        public IActionResult Items()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCart();
            var items = this.cartService.GetItems(shoppingCartId);
            var itemsIds = items.Select(x => x.ProductId);
            var itemQty = items.ToDictionary(x => x.ProductId, x => x.Quantity);
            var itemsDetails =this. db.Products
                .Where(p => itemsIds.Contains(p.Id))
                .Select(p => new CartItemViewModel
                {
                    ProductId = p.Id,
                    Make = p.Make,
                    Model = p.Model,
                    Price = p.Price,
                    Photo = p.Photo
                }).ToList();

            itemsDetails.ForEach(p => p.Quantity = itemQty[p.ProductId]);

            return View(itemsDetails);
        }

        public IActionResult AddToCart(int id)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCart();

            if (productService.HasEnough(id)) this.cartService.AddToCart(shoppingCartId, id);
            TempData.AddSuccessMessage("Successfully added an item to your shopping cart!");
            return RedirectToAction("AllProducts", "Store");
        }

        public IActionResult RemoveItem(int id)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCart();

            this.cartService.RemoveFromCart(shoppingCartId, id);
            TempData.AddSuccessMessage("Successfully removed an item from your shopping cart!");
            return RedirectToAction(nameof(Items));
        }
        
        [Authorize]
        public IActionResult FinishOrder()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCart();
            var items = this.cartService.GetItems(shoppingCartId);
            var itemsIds = items.Select(x => x.ProductId);
            var itemsToAdd = db.Products.Where(x => itemsIds.Contains(x.Id)).Select(x => x.Id).ToList();
            if (!items.Any())
            {
                return RedirectToAction("Items", nameof(ShoppingCart));

            }
            var userId = this.userService.GetUserId(User);

            foreach(var i in itemsToAdd)
            {
                using (db)
                {
                    var item = db.Products.Where(x => x.Id == i).FirstOrDefault();
                    var user = db.Users.Include(x => x.ProductsBougth).FirstOrDefault(x => x.Id == userId);

                    if (user.ProductsBougth.Where(x => x.Product == item).Count() == 0)
                    {
                        user.ProductsBougth.Add(new ProductBuyer { Product = item });
                        db.SaveChanges();
                    }
                }
            }                        
            cartService.Clear(shoppingCartId);
            TempData.AddSuccessMessage("Successfully purchased products from shopping cart!");
            return RedirectToAction("Items", nameof(ShoppingCart));
        }
    }
}
