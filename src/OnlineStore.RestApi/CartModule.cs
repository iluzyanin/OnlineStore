using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Responses.Negotiation;
using OnlineStore.Core.Models;

namespace OnlineStore.RestApi
{
    public class CartModule : NancyModule
    {
        private static IList<Cart> carts = new List<Cart>();
        private static int cartIndex = 1;

        public CartModule()
          : base("carts/")
        {
            this.Post("/", _ => CreateCart());
            this.Get("/{id}", parameters => GetCart(parameters.id));
        }

        public Negotiator CreateCart()
        {
            Cart newCart = new Cart();
            newCart.Id = cartIndex++;
            carts.Add(newCart);

            return Negotiate
                .WithHeader("Location", $"{Request.Url}/{newCart.Id}")
                .WithStatusCode(HttpStatusCode.Created);
        }

        public Negotiator GetCart(int id)
        {
            Cart cart = carts.FirstOrDefault(c => c.Id == id);
            if (cart == null)
            {
                return Negotiate
                    .WithAllowedMediaRange("application/json")
                    .WithModel(new { Error = $"Cart with id = {id} was not found" })
                    .WithStatusCode(HttpStatusCode.NotFound);
            }

            return Negotiate
                .WithAllowedMediaRange("application/json")
                .WithModel(cart);
        }
    }
}
