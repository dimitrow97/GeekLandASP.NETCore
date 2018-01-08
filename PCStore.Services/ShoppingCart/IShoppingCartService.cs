namespace PCStore.Services.ShoppingCart
{
    using PCStore.Services.ShoppingCart.Models;
    using System.Collections.Generic;

    public interface IShoppingCartService
    {
        void AddToCart(string id, int productId);

        void RemoveFromCart(string id, int productId);

        IEnumerable<CartItem> GetItems(string id); 
        
        void Clear(string id);
    }
}
