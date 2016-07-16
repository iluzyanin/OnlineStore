using System.Collections.Generic;
using Nancy;
using OnlineStore.Core.Models;

namespace OnlineStore.RestApi
{
    public class CartModule : NancyModule
    {
        private static IList<Cart> carts = new List<Cart>();

        public CartModule()
          :base("carts/")
        {
            Get("/", _ => "Stub for GET All endpoint");
        }
    }
}
