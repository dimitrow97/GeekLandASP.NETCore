namespace PCStore.Services.Admin
{
    using Microsoft.AspNetCore.Http;
    using PCStore.Data.Models;
    using PCStore.Services.Admin.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminProductService
    {
        Task<IEnumerable<AdminProductListingServiceModel>> AllAsync();
        Task<TModel> ByIdAsync<TModel>(int id) where TModel : class;
        Task<IEnumerable<AdminProductListingServiceModel>> AllAsyncRandomHome();
        Product FindById(int id);
        Task<string> EditProduct(int id, Product product);
        Task<string> DeleteProduct(int id);
        Task<string> AddProduct(Product product, IFormFile Photo);
        bool HasEnough(int id);
    }
}
