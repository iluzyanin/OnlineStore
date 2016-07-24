using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Responses.Negotiation;
using Nancy.ModelBinding;
using OnlineStore.Core.Models;
using OnlineStore.RestApi.DataTransfer;

namespace OnlineStore.RestApi.Modules
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
            this.Patch("/{id}", parameters => ApplyCoupon(parameters.id));
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
                return NotFoundResponse(id);
            }

            return Negotiate
                .WithAllowedMediaRange("application/json")
                .WithModel(cart);
        }

        public Negotiator ApplyCoupon(int id)
        {
            Cart cart = carts.FirstOrDefault(c => c.Id == id);
            if (cart == null)
            {
                return NotFoundResponse(id);
            }

            CouponDto couponDto = this.BindAndValidate<CouponDto>();
            if (!ModelValidationResult.IsValid)
            {
                return Negotiate
                    .WithModel(ModelValidationResult.Errors)
                    .WithStatusCode(HttpStatusCode.BadRequest);
            }

            return Negotiate
                .WithAllowedMediaRange("application/json")
                .WithModel(couponDto);
        }

        private Negotiator NotFoundResponse(int id)
        {
            return Negotiate
                .WithAllowedMediaRange("application/json")
                .WithModel(new { Error = $"Cart with id = {id} was not found" })
                .WithStatusCode(HttpStatusCode.NotFound);
        }
    }
}
