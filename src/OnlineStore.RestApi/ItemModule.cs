using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;
using Nancy.Responses.Negotiation;
using OnlineStore.Core.Models;

namespace OnlineStore.RestApi
{
    public class ItemModule : NancyModule
    {
        private static IList<Item> items = new List<Item>
        {
            new Item(1, "Apple", 3m),
            new Item(2, "Banana", 1.4m),
            new Item(3, "Orange", 2.8m),
        };

        public ItemModule()
            : base("/items")
        {
            this.Get("/", _ => GetAllItems());
            this.Get("/{id}", parameters => GetItem(parameters.id));
            this.Post("/", parameters => CreateItem(parameters));
            this.Delete("/{id}", parameters => DeleteItem(parameters.id));
        }

        public Negotiator GetAllItems()
        {
            return Negotiate
                .WithAllowedMediaRange("application/json")
                .WithModel(items);
        }

        public Negotiator GetItem(int id)
        {
            Item item = items.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return Negotiate
                    .WithAllowedMediaRange("application/json")
                    .WithModel(new { Error = $"Item with id = {id} was not found" })
                    .WithStatusCode(HttpStatusCode.NotFound);
            }

            return Negotiate
                .WithAllowedMediaRange("application/json")
                .WithModel(item);
        }

        public Negotiator CreateItem(dynamic parameters)
        {
            NewItemDto newItem = this.BindAndValidate<NewItemDto>();

            if (!ModelValidationResult.IsValid)
            {
                return Negotiate
                    .WithModel(ModelValidationResult.Errors)
                    .WithStatusCode(HttpStatusCode.BadRequest);
            }

            Item existingItem = items.FirstOrDefault(i => i.Name.Equals(newItem.Name, StringComparison.CurrentCultureIgnoreCase));
            if (existingItem != null)
            {
                return Negotiate
                    .WithAllowedMediaRange("application/json")
                    .WithModel(new { Error = $"Item with same name ({newItem.Name}) already exists" })
                    .WithStatusCode(HttpStatusCode.BadRequest);
            }

            int newId = items.Last().Id + 1;

            items.Add(new Item(newId, newItem.Name, newItem.Price));

            return Negotiate
                .WithHeader("Location", $"{Request.Url}/{newId}")
                .WithStatusCode(HttpStatusCode.Created);
        }

        public Negotiator DeleteItem(int id)
        {
            Item item = items.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return Negotiate
                    .WithAllowedMediaRange("application/json")
                    .WithModel(new { Error = $"Item with id = {id} was not found" })
                    .WithStatusCode(HttpStatusCode.NotFound);
            }

            items.Remove(item);

            return Negotiate
                .WithAllowedMediaRange("application/json")
                .WithStatusCode(HttpStatusCode.NoContent);
        }
    }
}