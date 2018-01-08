namespace PCStore.Services.Admin.Implementations
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using PCStore.Services.Admin.Models;
    using PCStore.Data;
    using AutoMapper.QueryableExtensions;
    using System;
    using PCStore.Data.Models;
    using Microsoft.AspNetCore.Http;
    using System.IO;

    public class AdminProductService : IAdminProductService
    {
        private readonly PCStoreDbContext db;

        public AdminProductService(PCStoreDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminProductListingServiceModel>> AllAsync()
            => await this.db
                .Products.OrderBy(r => Guid.NewGuid())
                .ProjectTo<AdminProductListingServiceModel>()
                .ToListAsync();

        public async Task<IEnumerable<AdminProductListingServiceModel>> AllAsyncRandomHome()
           => await this.db
               .Products.OrderBy(r => Guid.NewGuid()).Take(3)
               .ProjectTo<AdminProductListingServiceModel>()
               .ToListAsync();

        public async Task<TModel> ByIdAsync<TModel>(int id) where TModel : class
           => await this.db
               .Products
               .Where(c => c.Id == id)
               .ProjectTo<TModel>()
               .FirstOrDefaultAsync();

        public Product FindById(int id) => db.Products.Where(x => x.Id == id).FirstOrDefault();

        public bool HasEnough(int id)
        {
            var product = FindById(id);

            if (product.Quantity >= 1) return true;
            else return false;
        }

        public async Task<string> EditProduct(int id, Product product)
        {
            var productToEdit = FindById(id);

            productToEdit.Make = product.Make;
            productToEdit.Model = product.Model;
            productToEdit.Quantity = product.Quantity;
            productToEdit.Price = product.Price;
            productToEdit.Specs = product.Specs;

            try
            {
                using (db)
                {
                    db.Update(productToEdit);
                    await db.SaveChangesAsync();
                }
                return $"{product.Type} {product.Make} {product.Model} was successfully updated.";
            }
            catch
            {
                return $"{product.Type} {product.Make} {product.Model} was failed to be updated.";
            }
            
        }

        public async Task<string> DeleteProduct(int id)
        {
            var product = FindById(id);
            var productExists = product != null;

            if (!productExists)
            {
                return "Invalid identity details.";
            }

            using (db)
            {
                try
                {
                    db.Products.Remove(product);
                    await db.SaveChangesAsync();
                    return $"{product.Type} {product.Make} {product.Model} was successfully deleted.";
                }
                catch
                {
                    return $"The deleting process failed.";
                }
            }
        }

        public async Task<string> AddProduct(Product product, IFormFile Photo)
        {
            if (Photo.Length > 0 && Photo != null)
            {
                using (var stream = new MemoryStream())
                {
                    await Photo.CopyToAsync(stream);
                    product.Photo = stream.ToArray();
                }
            }

            if (db.Products.Where(x => x.Make == product.Make && x.Model == product.Model && x.Type == product.Type).Count() == 0)
                db.Products.Add(product);
            else
            {
                var dbProduct = db.Products.Where(x => x.Make == product.Make && x.Model == product.Model && x.Type == product.Type).FirstOrDefault();
                dbProduct.Quantity += product.Quantity;
            }

            try
            {
                using (db)
                {
                    await db.SaveChangesAsync();
                }
                return $"{product.Type} {product.Make} {product.Model} added successfully!";
            }
            catch
            {

                return $"{product.Type} {product.Make} {product.Model} failed to be added!";
            }
        }
    }
}


