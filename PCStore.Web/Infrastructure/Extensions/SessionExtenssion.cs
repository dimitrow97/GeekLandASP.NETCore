namespace PCStore.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Http;
    using PCStore.Services.ShoppingCart.Models;
    using System;

    public static class SessionExtenssion
    {
        private const string ShoppingCartId = "Shopping_Cart_Id";

        public static string GetShoppingCart(this ISession session)
        {
            var shoppingCartId = session.GetString(ShoppingCartId);

            if (shoppingCartId == null)
            {
                shoppingCartId = Guid.NewGuid().ToString();

                session.SetString(ShoppingCartId, shoppingCartId);
            }

            return shoppingCartId;
        }
    }
}
